using FlowerGame.Core;
using FlowerGame.Core.Domain;

var random = new Random(7);
var now = DateTimeOffset.UtcNow;

var player = new PlayerState
{
    Coins = 0,
    Crystals = 50,
    Water = 30,
    MaxWater = 30,
    Nutrients = 20,
    MaxNutrients = 20,
    LastWaterRegenAt = now,
    LastNutrientRegenAt = now
};

var prototype = new PrototypeLoop();
var result = prototype.ExecuteSingleCycle(player, now, random);

Console.WriteLine("=== Flower Shop Prototype (Unity C# Core Loop) ===");
Console.WriteLine($"Gacha x10 -> {string.Join(", ", result.Gacha.Select(x => $"{x.Seed.Name}(★{(int)x.Seed.Rarity}){(x.IsNew ? " NEW!" : string.Empty)}"))}");
Console.WriteLine($"Bloomed: {string.Join(", ", result.Bloomed.Select(x => x.Name))}");
Console.WriteLine($"Order: {result.Order.FlowerSeedId} x{result.Order.Quantity} (★{(int)result.Order.RequestedRarity})");

if (result.Reward is null)
{
    Console.WriteLine("Order not fulfilled this cycle.");
}
else
{
    Console.WriteLine($"Reward -> Coins:+{result.Reward.Coins} Crystals:+{result.Reward.Crystals} Water:+{result.Reward.Water} Nutrients:+{result.Reward.Nutrients}");
}

Console.WriteLine($"Player -> Coins:{player.Coins} Crystals:{player.Crystals} Water:{player.Water}/{player.MaxWater} Nutrients:{player.Nutrients}/{player.MaxNutrients}");
