using Microsoft.Xna.Framework;

namespace GenericCityBuilderRPG.General
{
    static class VirtualScreenSize
    {
        public static int Width { get; }
        public static int Height { get; }
        public static int ScreenSizeMultiplier { get; set; }

        static VirtualScreenSize()
        {
            Width = 480;
            Height = 270;
        }

        public static Rectangle CalculateVisibleArea(Vector2 playerPosition)
        {
            var screenSize = new Vector2(Width * ScreenSizeMultiplier, Height * ScreenSizeMultiplier);
            var rect = new Rectangle(
            (int)playerPosition.X - (int)screenSize.X / 2,
            (int)playerPosition.Y - (int)screenSize.Y / 2,
            (int)screenSize.X, (int)screenSize.Y);
            return rect;
        }

        public static Vector2 ScreenToWorld(Vector2 playerPosition, int x, int y)
        {
            var position = CalculateVisibleArea(playerPosition);

            return new Vector2(position.X + x, position.Y + y);
        }
    }
}
