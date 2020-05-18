using GenericCityBuilderRPG.Models;
using Microsoft.Xna.Framework;

namespace GenericCityBuilderRPG.General
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(PlayerModel player)
        {
            var position = Matrix.CreateTranslation(
                -player.Position.X - (player.Width / 2), 
                -player.Position.Y - (player.Height / 2), 
                0);

            var offset = Matrix.CreateTranslation(
                VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier  / 2, 
                VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2, 
                0);

            Transform = position * offset;
        }
    }
}
