
using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GenericCityBuilderRPG.Models
{
    class TerrainTileModel : IModel
    {
        public int Frame { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TargetPosition { get; set; }
        public Rectangle Area { get; set; }
        public Vector2 Scale { get; set; }
        public BiomeType Type { get; set; }

        public TerrainTileModel(int frame, int width, int height, BiomeType type, Vector2 position, Vector2 scale)
        {
            Frame = frame;
            Width = width;
            Height = height;
            Type = type;
            Position = position;
            Scale = scale;
            Area = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }
    }
}
