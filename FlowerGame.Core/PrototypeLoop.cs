using FlowerGame.Core.Domain;
using FlowerGame.Core.Systems;

namespace FlowerGame.Core;

public class PrototypeLoop
{
    private readonly GachaSystem _gachaSystem;
    private readonly GrowthSystem _growthSystem;
    private readonly OrderSystem _orderSystem;
    private readonly RewardSystem _rewardSystem;

    public PrototypeLoop(
        GachaSystem? gachaSystem = null,
        GrowthSystem? growthSystem = null,
        OrderSystem? orderSystem = null,
        RewardSystem? rewardSystem = null)
    {
        _gachaSystem = gachaSystem ?? new GachaSystem();
        _growthSystem = growthSystem ?? new GrowthSystem();
        _orderSystem = orderSystem ?? new OrderSystem();
        _rewardSystem = rewardSystem ?? new RewardSystem();
    }

    public (IReadOnlyList<GachaResult> Gacha, IReadOnlyList<SeedDefinition> Bloomed, CustomerOrder Order, RewardResult? Reward)
        ExecuteSingleCycle(PlayerState player, DateTimeOffset now, Random random)
    {
        var gachaResults = _gachaSystem.PullTen(player, random);
        var selectedSeed = gachaResults
            .Select(x => x.Seed)
            .OrderBy(x => x.Rarity)
            .First();

        _growthSystem.PlantSeed(player, selectedSeed, now);
        EnergySystem.Regenerate(player, now + selectedSeed.GrowthTime);
        var bloomed = _growthSystem.CollectBloomedFlowers(player, now + selectedSeed.GrowthTime);

        var order = _orderSystem.GenerateOrder(random);
        if (player.BloomInventory.GetValueOrDefault(order.FlowerSeedId, 0) < order.Quantity && bloomed.Count > 0)
        {
            var backupSeedId = bloomed[0].Id;
            order = order with { FlowerSeedId = backupSeedId, Quantity = 1, RequestedRarity = bloomed[0].Rarity };
        }

        if (!_orderSystem.TryFulfillOrder(player, order))
        {
            return (gachaResults, bloomed, order, null);
        }

        var reward = _rewardSystem.GrantReward(player, order);
        return (gachaResults, bloomed, order, reward);
    }
}
