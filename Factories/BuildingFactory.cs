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
                new BuildingModel(7, 128, 128, BuildingType.PoorHouse, ResourceType.Population, 1, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 50), new ResourceCostModel(ResourceType.Rock, 50) }, new ResourceCostModel(ResourceType.None, 0)),
                new BuildingModel(3, 128, 128, BuildingType.Farm, ResourceType.Food, 5, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 75), new ResourceCostModel(ResourceType.Water, 150) }, new ResourceCostModel(ResourceType.Water, 5)),
                new BuildingModel(13, 128, 128, BuildingType.Inn, ResourceType.Gold, 3, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 100), new ResourceCostModel(ResourceType.Coal, 50) }, new ResourceCostModel(ResourceType.Coal, 10)),
                new BuildingModel(21, 128, 128, BuildingType.Windmill, ResourceType.None, 0, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 120), new ResourceCostModel(ResourceType.Rock, 100) }, new ResourceCostModel(ResourceType.None, 0)),
                new BuildingModel(18, 128, 128, BuildingType.TownHall, ResourceType.Population, 3, new List<ResourceCostModel>{ new ResourceCostModel(ResourceType.Wood, 200), new ResourceCostModel(ResourceType.Gold, 100) }, new ResourceCostModel(ResourceType.Silver, 5))
            };

            return buildings;
        }
    }
}
