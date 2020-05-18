using Microsoft.Xna.Framework;

namespace GenericCityBuilderRPG.Interfaces
{
    interface IState
    {
        void Update(GameTime gameTime);

        void Draw();

        void Enter(params object[] args);

        void Exit();
    }
}
