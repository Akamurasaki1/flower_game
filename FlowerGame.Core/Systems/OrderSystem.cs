using FlowerGame.Core.Domain;

namespace FlowerGame.Core.Systems;

public class OrderSystem
{
    private readonly IReadOnlyList<SeedDefinition> _catalog;

    public OrderSystem(IReadOnlyList<SeedDefinition>? catalog = null)
    {
        _catalog = catalog ?? GameCatalog.Seeds;
    }

    public CustomerOrder GenerateOrder(Random random)
    {
        var rarity = RollRarity(random);
        var candidates = _catalog.Where(x => x.Rarity == rarity).ToArray();

        if (candidates.Length == 0)
        {
            candidates = _catalog.Where(x => x.Rarity == SeedRarity.Star1).ToArray();
            rarity = SeedRarity.Star1;
        }

        var seed = candidates[random.Next(candidates.Length)];
        var quantity = rarity switch
        {
            SeedRarity.Star1 => random.Next(1, 4),
            SeedRarity.Star2 => random.Next(1, 3),
            _ => 1
        };

        return new CustomerOrder(Guid.NewGuid(), seed.Id, quantity, rarity);
    }

    public bool TryFulfillOrder(PlayerState player, CustomerOrder order)
    {
        player.BloomInventory.TryGetValue(order.FlowerSeedId, out var count);
        if (count < order.Quantity)
        {
            return false;
        }

        player.BloomInventory[order.FlowerSeedId] = count - order.Quantity;
        return true;
    }

    private static SeedRarity RollRarity(Random random)
    {
        var value = random.NextDouble();
        return value switch
        {
            < 0.65 => SeedRarity.Star1,
            < 0.90 => SeedRarity.Star2,
            < 0.98 => SeedRarity.Star4,
            _ => SeedRarity.Star5
        };
    }
}
