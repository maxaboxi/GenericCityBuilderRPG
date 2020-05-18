using GenericCityBuilderRPG.Models;
using GenericCityBuilderRPG.Views;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GenericLooterShooterRPG.Views
{
    class PlayerResourcesView : BaseView
    {
        private readonly PlayerResourcesModel _playerResourcesModel;
        public PlayerResourcesView(ContentManager contentManager, SpriteBatch spriteBatch, PlayerResourcesModel playerResourcesModel) : base(contentManager, spriteBatch)
        {
            _playerResourcesModel = playerResourcesModel;
        }

        public override void Draw()
        {
        }
    }
}
