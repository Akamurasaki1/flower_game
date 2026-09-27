namespace FlowerGame.Core.Domain;

public enum SeedRarity
{
    Star1 = 1,
    Star2 = 2,
    Star3 = 3,
    Star4 = 4,
    Star5 = 5
}

public record SeedDefinition(
    string Id,
    string Name,
    SeedRarity Rarity,
    TimeSpan GrowthTime,
    int WaterCost,
    int NutrientCost);

public record GachaResult(SeedDefinition Seed, bool IsNew);

public record FlowerSlot(
    Guid SlotId,
    SeedDefinition Seed,
    DateTimeOffset PlantedAt,
    DateTimeOffset BloomAt)
{
    public bool IsBloomed(DateTimeOffset now) => now >= BloomAt;
}

public record CustomerOrder(Guid Id, string FlowerSeedId, int Quantity, SeedRarity RequestedRarity);

public record RewardResult(int Coins, int Crystals, int Water, int Nutrients, int GachaTickets);

public class PlayerState
{
    public int Coins { get; set; }
    public int Crystals { get; set; }

    public int Water { get; set; }
    public int MaxWater { get; set; }

    public int Nutrients { get; set; }
    public int MaxNutrients { get; set; }

    public DateTimeOffset LastWaterRegenAt { get; set; }
    public DateTimeOffset LastNutrientRegenAt { get; set; }

    public Dictionary<string, int> SeedInventory { get; } = new();
    public Dictionary<string, int> BloomInventory { get; } = new();
    public HashSet<string> DiscoveredFlowerIds { get; } = [];
    public List<FlowerSlot> ActiveSlots { get; } = [];
}
