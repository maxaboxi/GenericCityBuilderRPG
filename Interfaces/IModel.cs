using Microsoft.Xna.Framework;

namespace GenericCityBuilderRPG.Interfaces
{
    public interface IModel
    {
        int Frame { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Vector2 Position { get; set; }
        Rectangle Area { get; set; }
        Vector2 Scale { get; set; }
    }
}
