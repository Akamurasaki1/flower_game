using FlowerGame.Core;
using FlowerGame.Core.Domain;
using FlowerGame.Core.Systems;

namespace FlowerGame.Core.Tests;

public class PrototypeTests
{
    [Fact]
    public void Gacha_Should_Favor_Common_Seeds()
    {
        var player = CreatePlayer();
        var gacha = new GachaSystem();
        var random = new Random(42);

        var common = 0;
        var rare = 0;

        for (var i = 0; i < 5000; i++)
        {
            var result = gacha.PullSingle(player, random);
            if (result.Seed.Rarity <= SeedRarity.Star2) common++;
            if (result.Seed.Rarity >= SeedRarity.Star4) rare++;
        }

        Assert.True(common > rare);
    }

    [Fact]
    public void Orders_Should_Request_Star1_More_Than_Half()
    {
        var orderSystem = new OrderSystem();
        var random = new Random(9);

        var star1 = 0;
        const int sample = 1000;

        for (var i = 0; i < sample; i++)
        {
            var order = orderSystem.GenerateOrder(random);
            if (order.RequestedRarity == SeedRarity.Star1)
            {
                star1++;
            }
        }

        var ratio = (double)star1 / sample;
        Assert.InRange(ratio, 0.60, 0.70);
    }

    [Fact]
    public void Loop_Should_Produce_Reward_When_Order_Is_Fulfilled()
    {
        var prototype = new PrototypeLoop();
        var now = DateTimeOffset.UtcNow;
        var random = new Random(13);

        var player = CreatePlayer(now);
        var result = prototype.ExecuteSingleCycle(player, now, random);

        Assert.NotEmpty(result.Gacha);
        Assert.NotEmpty(result.Bloomed);
        Assert.NotNull(result.Reward);
        Assert.True(player.Coins > 0);
    }

    private static PlayerState CreatePlayer(DateTimeOffset? now = null)
    {
        var time = now ?? DateTimeOffset.UtcNow;
        return new PlayerState
        {
            Water = 30,
            MaxWater = 30,
            Nutrients = 20,
            MaxNutrients = 20,
            LastWaterRegenAt = time,
            LastNutrientRegenAt = time
        };
    }
}
