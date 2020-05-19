using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Interfaces;
using GenericCityBuilderRPG.Models;
using GenericLooterShooterRPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace GenericLooterShooterRPG.Controllers
{
    class BuildController : IController
    {
        private readonly BuildingListModel _buildingListModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerResourcesModel _playerResourceModel;
        private float _clickCooldownPeriod = 200f;
        private float _clickCooldown = 0f;
        private float _keyCooldownPeriod = 200f;
        private float _keyCooldown = 0f;

        public BuildController(BuildingListModel buildingListModel, PlayerModel playerModel, PlayerResourcesModel playerResourcesModel)
        {
            _buildingListModel = buildingListModel;
            _playerModel = playerModel;
            _playerResourceModel = playerResourcesModel;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var keyState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var mousePoint = mouseState.Position;
            var mousePosition = VirtualScreenSize.ScreenToWorld(_playerModel.Position, mousePoint.X, mousePoint.Y);

            foreach (var building in _buildingListModel.AvailableBuildings)
            {
                if (building.Area.Contains(mousePosition))
                {
                    building.ShowTooltip = true;
                }
                else
                {
                    building.ShowTooltip = false;
                }

                if (_clickCooldown > 0f)
                {
                    _clickCooldown -= deltaTime;
                    return;
                }

                if (building.IsSelected && mouseState.LeftButton == ButtonState.Pressed)
                {
                    foreach (var c in building.Cost)
                    {
                        _playerResourceModel.AddResource(c.Type, -c.Amount);
                    }
                    _clickCooldown = _clickCooldownPeriod;
                    building.IsSelected = false;
                    var b = new BuildingModel(building.Frame, building.Width, building.Height, building.Type, building.Cost)
                    {
                        Position = new Vector2((int)mousePosition.X, (int)mousePosition.Y)
                    };
                    _buildingListModel.BuiltBuildings.Add(b);
                }

                if (building.Area.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _clickCooldown = _clickCooldownPeriod;
                    if (_playerResourceModel.HasEnoughResources(building.Cost))
                    {
                        building.IsSelected = !building.IsSelected;
                    }
                }


            }

            if (_keyCooldown > 0f)
            {
                _keyCooldown -= deltaTime;
                return;
            }

            if (keyState.GetPressedKeys().Contains(Keys.Tab))
            {
                foreach (var building in _buildingListModel.AvailableBuildings)
                {
                    building.IsSelected = false;
                    _keyCooldown = _keyCooldownPeriod;
                }
            }

        }
    }
}
