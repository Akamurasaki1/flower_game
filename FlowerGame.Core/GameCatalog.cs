using FlowerGame.Core.Domain;

namespace FlowerGame.Core;

public static class GameCatalog
{
    public static readonly SeedDefinition Tulip = new(
        Id: "tulip_common",
        Name: "チューリップの種",
        Rarity: SeedRarity.Star1,
        GrowthTime: TimeSpan.FromMinutes(5),
        WaterCost: 2,
        NutrientCost: 1);

    public static readonly SeedDefinition Rose = new(
        Id: "rose_common",
        Name: "バラの種",
        Rarity: SeedRarity.Star2,
        GrowthTime: TimeSpan.FromMinutes(8),
        WaterCost: 3,
        NutrientCost: 2);

    public static readonly SeedDefinition Sunflower = new(
        Id: "sunflower_common",
        Name: "ヒマワリの種",
        Rarity: SeedRarity.Star3,
        GrowthTime: TimeSpan.FromMinutes(12),
        WaterCost: 4,
        NutrientCost: 3);

    public static readonly SeedDefinition TulipRare = new(
        Id: "tulip_rare",
        Name: "チューリップのレア種",
        Rarity: SeedRarity.Star4,
        GrowthTime: TimeSpan.FromMinutes(30),
        WaterCost: 6,
        NutrientCost: 5);

    public static readonly SeedDefinition AuroraLily = new(
        Id: "aurora_lily",
        Name: "オーロラリリー",
        Rarity: SeedRarity.Star5,
        GrowthTime: TimeSpan.FromHours(1),
        WaterCost: 8,
        NutrientCost: 8);

    public static readonly IReadOnlyList<SeedDefinition> Seeds =
    [
        Tulip,
        Rose,
        Sunflower,
        TulipRare,
        AuroraLily
    ];
}
