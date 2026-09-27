using FlowerGame.Core.Domain;

namespace FlowerGame.Core.Systems;

public class GrowthSystem
{
    public FlowerSlot PlantSeed(PlayerState player, SeedDefinition seed, DateTimeOffset now)
    {
        if (player.Water < seed.WaterCost || player.Nutrients < seed.NutrientCost)
        {
            throw new InvalidOperationException("水または栄養が不足しています。");
        }

        player.SeedInventory.TryGetValue(seed.Id, out var count);
        if (count <= 0)
        {
            throw new InvalidOperationException("対象の種子を所持していません。");
        }

        player.Water -= seed.WaterCost;
        player.Nutrients -= seed.NutrientCost;
        player.SeedInventory[seed.Id] = count - 1;

        var slot = new FlowerSlot(Guid.NewGuid(), seed, now, now + seed.GrowthTime);
        player.ActiveSlots.Add(slot);
        return slot;
    }

    public IReadOnlyList<SeedDefinition> CollectBloomedFlowers(PlayerState player, DateTimeOffset now)
    {
        var bloomed = player.ActiveSlots.Where(x => x.IsBloomed(now)).ToList();
        foreach (var slot in bloomed)
        {
            player.BloomInventory.TryGetValue(slot.Seed.Id, out var count);
            player.BloomInventory[slot.Seed.Id] = count + 1;
            player.ActiveSlots.Remove(slot);
        }

        return bloomed.Select(x => x.Seed).ToList();
    }
}
