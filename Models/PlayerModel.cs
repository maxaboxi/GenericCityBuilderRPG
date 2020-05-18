using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.Interfaces;
using GenericLooterShooterRPG.Models;
using Microsoft.Xna.Framework;

namespace GenericCityBuilderRPG.Models
{
    public class PlayerModel : IModel
    {
        public int Frame { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 TargetPosition { get; set; }
        public Rectangle Area { get; set; }
        public Vector2 Position { get; set; }
        public float Damage { get; set; }
        public float DamageStartingValue { get; set; }
        public float Speed { get; set; }
        public ResourceHarvester ResourceHarvester { get; set; }


        public PlayerModel()
        {
            Position = SetStartingPosition();
            Speed = 350f;
            Frame = 0;
            Width = 24;
            Height = 32;
            Area = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            Scale = new Vector2(2, 2);
            ResourceHarvester = new ResourceHarvester(1);
        }

        private Vector2 SetStartingPosition()
        {
            var x = (int)MapSize.Width / 2;
            var y = (int)MapSize.Height / 2;
            return new Vector2(x, y);
        }
    }
}
