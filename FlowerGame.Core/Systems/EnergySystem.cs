using FlowerGame.Core.Domain;

namespace FlowerGame.Core.Systems;

public static class EnergySystem
{
    public static readonly TimeSpan WaterRegenInterval = TimeSpan.FromMinutes(2);
    public static readonly TimeSpan NutrientRegenInterval = TimeSpan.FromMinutes(3);

    public static void Regenerate(PlayerState player, DateTimeOffset now)
    {
        if (player.Water < player.MaxWater)
        {
            var elapsedWaterTicks = (now - player.LastWaterRegenAt).Ticks;
            var waterGained = (int)(elapsedWaterTicks / WaterRegenInterval.Ticks);
            if (waterGained > 0)
            {
                player.Water = Math.Min(player.MaxWater, player.Water + waterGained);
                player.LastWaterRegenAt = player.LastWaterRegenAt.AddTicks((long)waterGained * WaterRegenInterval.Ticks);
            }
        }
        else
        {
            player.LastWaterRegenAt = now;
        }

        if (player.Nutrients < player.MaxNutrients)
        {
            var elapsedNutrientTicks = (now - player.LastNutrientRegenAt).Ticks;
            var nutrientsGained = (int)(elapsedNutrientTicks / NutrientRegenInterval.Ticks);
            if (nutrientsGained > 0)
            {
                player.Nutrients = Math.Min(player.MaxNutrients, player.Nutrients + nutrientsGained);
                player.LastNutrientRegenAt = player.LastNutrientRegenAt.AddTicks((long)nutrientsGained * NutrientRegenInterval.Ticks);
            }
        }
        else
        {
            player.LastNutrientRegenAt = now;
        }
    }
}
