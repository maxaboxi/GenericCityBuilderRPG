using System.Collections.Generic;

namespace GenericLooterShooterRPG.Models
{
    class BuildingListModel
    {
        public List<BuildingModel> AvailableBuildings { get; } = new List<BuildingModel>();
        public List<BuildingModel> BuiltBuildings { get; } = new List<BuildingModel>();

        public BuildingListModel(IEnumerable<BuildingModel> availableBuildings)
        {
            AvailableBuildings.AddRange(availableBuildings);
        }

        public void AddAvailableBuilding(BuildingModel building)
        {
            AvailableBuildings.Add(building);
        }

        public void AddBuiltBuilding(BuildingModel building)
        {
            BuiltBuildings.Add(building);
        }
    }
}
