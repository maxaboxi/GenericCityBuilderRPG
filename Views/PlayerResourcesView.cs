using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Models;
using GenericCityBuilderRPG.Views;
using GenericLooterShooterRPG.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GenericLooterShooterRPG.Views
{
    class PlayerResourcesView : BaseView
    {
        private readonly PlayerResourcesModel _playerResourcesModel;
        private readonly PlayerModel _playerModel;
        private readonly SpriteSheet _water;
        private readonly SpriteSheet _tree;
        private readonly SpriteSheet _bar;
        private readonly SpriteSheet _minerals;
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _gameFont;

        private Vector2 _position;
        private Vector2 _positionOffset = new Vector2(80, 0);
        private Vector2 _textOffset = new Vector2(50, 0);
        public PlayerResourcesView(ContentManager contentManager, SpriteBatch spriteBatch, PlayerResourcesModel playerResourcesModel, PlayerModel playerModel) : base(contentManager, spriteBatch)
        {
            _playerResourcesModel = playerResourcesModel;
            _playerModel = playerModel;

            var treeTextures = contentManager.Load<Texture2D>("trees");
            var waterTexture = contentManager.Load<Texture2D>("water");
            var mineralTextures = contentManager.Load<Texture2D>("minerals");
            var barTexture = contentManager.Load<Texture2D>("resbarbackground");
            _water = new SpriteSheet(spriteBatch, waterTexture, (int)TerrainTileModelSize.Width, (int)TerrainTileModelSize.Height);
            _tree = new SpriteSheet(spriteBatch, treeTextures, (int)ResourceSize.TreeWidth, (int)ResourceSize.TreeHeight);
            _minerals = new SpriteSheet(spriteBatch, mineralTextures, (int)ResourceSize.MineralWidth, (int)ResourceSize.MineralHeight);
            _bar = new SpriteSheet(spriteBatch, barTexture, 256, 256);

            _spriteBatch = spriteBatch;
            _gameFont = contentManager.Load<SpriteFont>("FontFile");
        }

        public override void Draw()
        {
            var visibleArea = VirtualScreenSize.CalculateVisibleArea(_playerModel.Position);
            _position = new Vector2(visibleArea.X, visibleArea.Y + 20);
            int i = 0;
            while (i <= visibleArea.Width)
            {
                _bar.Draw(new Vector2(_position.X + i, visibleArea.Y + 5), 0, Color.White, new Vector2(0.25f, 0.25f));
                i += 64;
            }
            _water.Draw(_position + new Vector2(15,0), 0, Color.White, new Vector2(0.25f, 0.25f));
            _spriteBatch.DrawString(_gameFont, 100.ToString(), _position + _textOffset, Color.White);
            _tree.Draw(_position + _positionOffset * 2, 7, Color.White, new Vector2(0.25f, 0.25f));
            //_minerals.Draw(_position, resource.Frame, resource.Amount > 0 ? Color.White : Color.Red);

        }
    }
}
