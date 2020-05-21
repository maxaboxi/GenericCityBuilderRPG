using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Models;
using GenericCityBuilderRPG.Views;
using GenericLooterShooterRPG.Enums;
using GenericLooterShooterRPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GenericLooterShooterRPG.Views
{
    class BuildView : BaseView
    {
        private readonly SpriteSheet _buildings;
        private readonly SpriteSheet _bar;

        private readonly BuildingListModel _buildingListModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerResourcesModel _playerResourcesModel;

        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _gameFont;
        private readonly SpriteFont _gameFontSmall;

        public BuildView(ContentManager contentManager, SpriteBatch spriteBatch, BuildingListModel buildingListModel, PlayerModel playerModel, PlayerResourcesModel playerResourcesModel) : base(contentManager, spriteBatch)
        {
            var buildingTextures = contentManager.Load<Texture2D>("buildings");
            var barTexture = contentManager.Load<Texture2D>("resbarbackground");
            _buildings = new SpriteSheet(spriteBatch, buildingTextures, 128, 128);
            _bar = new SpriteSheet(spriteBatch, barTexture, 256, 256);

            _buildingListModel = buildingListModel;
            _playerModel = playerModel;
            _playerResourcesModel = playerResourcesModel;

            _spriteBatch = spriteBatch;
            _gameFont = contentManager.Load<SpriteFont>("FontFile");
            _gameFontSmall = contentManager.Load<SpriteFont>("font_small");
        }

        public override void Draw()
        {
            var visibleArea = VirtualScreenSize.CalculateVisibleArea(_playerModel.Position);
            var mouseState = Mouse.GetState();
            var mousePoint = mouseState.Position;
            var mousePosition = VirtualScreenSize.ScreenToWorld(_playerModel.Position, mousePoint.X, mousePoint.Y);

            var position = new Vector2(visibleArea.X, visibleArea.Bottom);

            int i = 0;
            while (i <= visibleArea.Width)
            {
                _bar.Draw(new Vector2(position.X + i, visibleArea.Bottom - 47), 0, Color.White, new Vector2(0.25f, 0.25f));
                i += 64;
            }

            var j = 15;
            foreach (var building in _buildingListModel.AvailableBuildings)
            {
                building.Position = new Vector2(position.X + j, visibleArea.Bottom - 47);
                building.Area = new Rectangle((int)building.Position.X, (int)building.Position.Y, 64, 64);
                _buildings.Draw(building.Position, building.Frame, _playerResourcesModel.HasEnoughResources(building.Cost) ? Color.White : Color.Red, new Vector2(0.5f, 0.5f));
                j += 68;

                if (building.ShowTooltip)
                {
                    ShowTooltip(building);
                }

                if (building.IsSelected)
                {
                    _buildings.Draw(mousePosition, building.Frame, building.CanBuild ? Color.White : Color.Red);
                }
            }

            foreach (var b in _buildingListModel.BuiltBuildings)
            {
                _buildings.Draw(b.Position, b.Frame, Color.White);
            }

            
        }

        private void ShowTooltip(BuildingModel building)
        {
            // TODO: Show upkeep cost w/ red
            var type = building.Type.ToString();
            if (building.Type == BuildingType.House || building.Type == BuildingType.PoorHouse)
            {
                type = "House";
            }
            _spriteBatch.DrawString(_gameFontSmall, type, building.Position + new Vector2(0, -65), Color.White);
            var k = 0;
            foreach (var c in building.Cost)
            {
                _spriteBatch.DrawString(_gameFontSmall, c.Type.ToString() + ": " + c.Amount.ToString(), building.Position + new Vector2(0, -50 - k), Color.White);
                k -= 15;
            }
        }
    }
}
