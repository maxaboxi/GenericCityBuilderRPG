using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.Interfaces;
using GenericLooterShooterRPG.Enums;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GenericLooterShooterRPG.Models
{
    class BuildingModel : IModel
    {
        public int Frame { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Area { get; set; }
        public Vector2 Scale { get; set; }
        public BuildingType Type { get; set; }
        public bool ShowTooltip { get; set; }
        public bool IsSelected { get; set; }
        public bool CanBuild { get; set; }
        public float ResourceCooldown { get; set; }
        public float ResourceGenCooldown { get; set; }
        public ResourceType ResourceType { get; set; }
        public int ResourceAmount { get; set; }
        public ResourceCostModel UpkeepCost { get; set; }
        public List<ResourceCostModel> Cost { get; set; }


        public BuildingModel(int frame, int width, int height, BuildingType type, float resourceGenCooldown, ResourceType resourceType, int resourceAmount, List<ResourceCostModel> cost, ResourceCostModel upkeepCost )
        {
            Frame = frame;
            Width = width;
            Height = height;
            Type = type;
            ResourceCooldown = 0;
            ResourceGenCooldown = resourceGenCooldown;
            ResourceType = resourceType;
            ResourceAmount = resourceAmount;
            Cost = cost;
            UpkeepCost = upkeepCost;
            IsSelected = false;
            CanBuild = true;
        }
    }
}
