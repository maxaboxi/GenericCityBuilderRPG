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
        private readonly SpriteSheet _terrain;
        private readonly SpriteSheet _water;
        private readonly SpriteSheet _tree;
        private readonly SpriteSheet _bar;
        private readonly SpriteSheet _minerals;
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _gameFont;
        public PlayerResourcesView(ContentManager contentManager, SpriteBatch spriteBatch, PlayerResourcesModel playerResourcesModel, PlayerModel playerModel) : base(contentManager, spriteBatch)
        {
            _playerResourcesModel = playerResourcesModel;
            _playerModel = playerModel;

            var terrainTextures = contentManager.Load<Texture2D>("terrain");
            var treeTextures = contentManager.Load<Texture2D>("trees");
            var waterTexture = contentManager.Load<Texture2D>("water");
            var mineralTextures = contentManager.Load<Texture2D>("minerals");
            var barTexture = contentManager.Load<Texture2D>("resbarbackground");
            _terrain = new SpriteSheet(spriteBatch, terrainTextures, (int)TerrainTileModelSize.Width, (int)TerrainTileModelSize.Height);
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
            var position = new Vector2(visibleArea.X, visibleArea.Y + 20);

            int i = 0;
            while (i <= visibleArea.Width)
            {
                _bar.Draw(new Vector2(position.X + i, visibleArea.Y + 5), 0, Color.White, new Vector2(0.25f, 0.25f));
                i += 64;
            }

            // Water
            _water.Draw(position + new Vector2(15,0), 0, Color.White, new Vector2(0.125f, 0.125f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Water.ToString(), position + new Vector2(55, 0), _playerResourcesModel.Water >= 0 ? Color.White : Color.Red);

            // Food
            // TODO: Change sprite
            _water.Draw(position + new Vector2(160, 0), 0, Color.White, new Vector2(0.125f, 0.125f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Food.ToString(), position + new Vector2(200, 0), _playerResourcesModel.Food >= 0 ? Color.White : Color.Red);

            // Sand
            _terrain.Draw(position + new Vector2(320, 0), 0, Color.White, new Vector2(0.125f, 0.125f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Sand.ToString(), position + new Vector2(360, 0), _playerResourcesModel.Sand >= 0 ? Color.White : Color.Red);

            // Rock
            _terrain.Draw(position + new Vector2(480, 0), 2, Color.White, new Vector2(0.125f, 0.125f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Rock.ToString(), position + new Vector2(520, 0), _playerResourcesModel.Rock >= 0 ? Color.White : Color.Red);

            // Wood
            _tree.Draw(position + new Vector2(640, 0), 7, Color.White, new Vector2(0.125f, 0.125f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Wood.ToString(), position + new Vector2(680, 0), _playerResourcesModel.Wood >= 0 ? Color.White : Color.Red);

            // Coal
            _minerals.Draw(position + new Vector2(800, 0), 0, Color.White, new Vector2(0.5f, 0.5f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Coal.ToString(), position + new Vector2(840, 0), _playerResourcesModel.Coal >= 0 ? Color.White : Color.Red);

            // Copper
            _minerals.Draw(position + new Vector2(960, 0), 1, Color.White, new Vector2(0.5f, 0.5f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Copper.ToString(), position + new Vector2(1000, 0), _playerResourcesModel.Copper >= 0 ? Color.White : Color.Red);

            // Silver
            _minerals.Draw(position + new Vector2(1120, 0), 6, Color.White, new Vector2(0.5f, 0.5f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Silver.ToString(), position + new Vector2(1160, 0), _playerResourcesModel.Silver >= 0 ? Color.White : Color.Red);

            // Gold
            _minerals.Draw(position + new Vector2(1280, 0), 3, Color.White, new Vector2(0.5f, 0.5f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Gold.ToString(), position + new Vector2(1320, 0), _playerResourcesModel.Gold >= 0 ? Color.White : Color.Red);

            // Diamond
            _minerals.Draw(position + new Vector2(1440, 0), 2, Color.White, new Vector2(0.5f, 0.5f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Diamond.ToString(), position + new Vector2(1480, 0), _playerResourcesModel.Diamond >= 0 ? Color.White : Color.Red);

            // Population
            // TODO: Change sprite
            _water.Draw(position + new Vector2(1600, 0), 0, Color.White, new Vector2(0.125f, 0.125f));
            _spriteBatch.DrawString(_gameFont, _playerResourcesModel.Population.ToString() + "/" + _playerResourcesModel.PopulationLimit.ToString(), position + new Vector2(1640, 0), Color.White);
        }
    }
}
