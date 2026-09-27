using FlowerGame.Core.Domain;

namespace FlowerGame.Core.Systems;

public class GachaSystem
{
    private readonly IReadOnlyList<(SeedDefinition Seed, int Weight)> _entries;

    public GachaSystem(IReadOnlyList<(SeedDefinition Seed, int Weight)>? entries = null)
    {
        _entries = entries ??
        [
            (GameCatalog.Tulip, 45),
            (GameCatalog.Rose, 25),
            (GameCatalog.Sunflower, 18),
            (GameCatalog.TulipRare, 9),
            (GameCatalog.AuroraLily, 3)
        ];
    }

    public GachaResult PullSingle(PlayerState player, Random random)
    {
        var totalWeight = _entries.Sum(x => x.Weight);
        var roll = random.Next(1, totalWeight + 1);

        var cumulative = 0;
        foreach (var entry in _entries)
        {
            cumulative += entry.Weight;
            if (roll <= cumulative)
            {
                var isNew = player.DiscoveredFlowerIds.Add(entry.Seed.Id);
                player.SeedInventory.TryGetValue(entry.Seed.Id, out var count);
                player.SeedInventory[entry.Seed.Id] = count + 1;
                return new GachaResult(entry.Seed, isNew);
            }
        }

        throw new InvalidOperationException("No gacha entry matched the roll.");
    }

    public IReadOnlyList<GachaResult> PullTen(PlayerState player, Random random)
    {
        var results = new List<GachaResult>(10);
        for (var i = 0; i < 10; i++)
        {
            results.Add(PullSingle(player, random));
        }

        return results;
    }
}
