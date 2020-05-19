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
                new BuildingModel(1, 128, 128, BuildingType.Farm, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 50) }),
                new BuildingModel(2, 128, 128, BuildingType.Inn, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 100), new ResourceCostModel(ResourceType.Coal, 50) }),
                new BuildingModel(13, 128, 128, BuildingType.Windmill, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 120), new ResourceCostModel(ResourceType.Rock, 100) }),
                new BuildingModel(10, 128, 128, BuildingType.TownHall, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 200), new ResourceCostModel(ResourceType.Gold, 100) })
            };

            return buildings;
        }
    }
}
