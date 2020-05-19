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
        public bool RequirementsMet { get; set; }
        public int ResourceGenCooldown { get; set; }
        public List<ResourceCostModel> Cost { get; set; }


        public BuildingModel(int frame, int width, int height, BuildingType type, int resourceGenCooldown, List<ResourceCostModel> cost )
        {
            Frame = frame;
            Width = width;
            Height = height;
            Type = type;
            ResourceGenCooldown = resourceGenCooldown;
            Cost = cost;
            IsSelected = false;
        }
    }
}
