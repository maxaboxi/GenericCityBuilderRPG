using GenericCityBuilderRPG.Enums;
using GenericLooterShooterRPG.Enums;
using GenericLooterShooterRPG.Models;
using System.Collections.Generic;

namespace GenericLooterShooterRPG.Factories
{
    static class BuildingFactory
    {
        public static List<BuildingModel> GenerateAvailableBuildings()
        {
            var buildings = new List<BuildingModel>
            {
                new BuildingModel(7, 128, 128, BuildingType.PoorHouse, 0, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 50), new ResourceCostModel(ResourceType.Rock, 50) }),
                new BuildingModel(3, 128, 128, BuildingType.Farm, 0, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 75), new ResourceCostModel(ResourceType.Water, 150) }),
                new BuildingModel(13, 128, 128, BuildingType.Inn, 60, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 100), new ResourceCostModel(ResourceType.Coal, 50) }),
                new BuildingModel(21, 128, 128, BuildingType.Windmill, 0, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 120), new ResourceCostModel(ResourceType.Rock, 100) }),
                new BuildingModel(18, 128, 128, BuildingType.TownHall, 0, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 200), new ResourceCostModel(ResourceType.Gold, 100) })
            };

            return buildings;
        }
    }
}
