using GenericCityBuilderRPG.Enums;
using GenericLooterShooterRPG.Enums;
using Microsoft.Xna.Framework;

namespace GenericCityBuilderRPG.Models
{
    class ResourceModel
    {
        public int Frame { get; set; }
        public ResourceType Type { get; set; }
        public int Amount { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Area { get; set; }
        public Rectangle TileArea { get; set; }
        public bool AmountVisible { get; set; }

        public ResourceModel(int frame, ResourceType type, int amount, Vector2 position, Rectangle tileArea)
        {
            Frame = frame;
            Type = type;
            Amount = amount;
            Position = position;
            Area = CalculateResourceArea();
            TileArea = tileArea;
            AmountVisible = false;
        }

        private Rectangle CalculateResourceArea()
        {
            int width;
            int height;

            switch (Type)
            {
                case ResourceType.Water:
                case ResourceType.Sand:
                case ResourceType.Rock:
                    width = (int)TerrainTileModelSize.Width;
                    height = (int)TerrainTileModelSize.Height;
                    break;
                case ResourceType.Wood:
                    width = (int)ResourceSize.TreeWidth;
                    height = (int)ResourceSize.TreeHeight;
                    break;
                case ResourceType.Coal:
                case ResourceType.Copper:
                case ResourceType.Diamond:
                case ResourceType.Gold:
                case ResourceType.Silver:
                    width = (int)ResourceSize.MineralWidth;
                    height = (int)ResourceSize.MineralHeight;
                    break;
                default:
                    width = (int)TerrainTileModelSize.Width;
                    height = (int)TerrainTileModelSize.Height;
                    break;
            }

            return new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }
    }
}
