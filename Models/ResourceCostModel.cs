using GenericCityBuilderRPG.Enums;

namespace GenericLooterShooterRPG.Models
{
    public class ResourceCostModel
    {
        public ResourceType Type { get; set; }
        public int Amount { get; set; }

        public ResourceCostModel(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
