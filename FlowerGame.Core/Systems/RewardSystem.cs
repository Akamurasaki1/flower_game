using FlowerGame.Core.Domain;

namespace FlowerGame.Core.Systems;

public class RewardSystem
{
    public RewardResult GrantReward(PlayerState player, CustomerOrder order)
    {
        var reward = order.RequestedRarity switch
        {
            SeedRarity.Star1 => new RewardResult(Coins: 120, Crystals: 1, Water: 1, Nutrients: 1, GachaTickets: 0),
            SeedRarity.Star2 => new RewardResult(Coins: 200, Crystals: 2, Water: 1, Nutrients: 1, GachaTickets: 0),
            SeedRarity.Star3 or SeedRarity.Star4 => new RewardResult(Coins: 500, Crystals: 5, Water: 3, Nutrients: 3, GachaTickets: 1),
            _ => new RewardResult(Coins: 1000, Crystals: 10, Water: 5, Nutrients: 5, GachaTickets: 2)
        };

        player.Coins += reward.Coins;
        player.Crystals += reward.Crystals;
        player.Water = Math.Min(player.MaxWater, player.Water + reward.Water);
        player.Nutrients = Math.Min(player.MaxNutrients, player.Nutrients + reward.Nutrients);
        return reward;
    }
}
