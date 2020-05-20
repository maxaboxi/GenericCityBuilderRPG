using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Interfaces;
using GenericCityBuilderRPG.Models;
using GenericLooterShooterRPG.Enums;
using GenericLooterShooterRPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
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
            AvailableBuildings(gameTime);
            BuiltBuildings(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var keyState = Keyboard.GetState();

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

        private void AvailableBuildings(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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

                var pos = new Vector2((int)mousePosition.X, (int)mousePosition.Y);
                if (building.IsSelected)
                {
                    if (IntersectsAnotherBuilding(new Rectangle((int)pos.X, (int)pos.Y, building.Width, building.Height)))
                    {
                        building.CanBuild = false;
                    } else
                    {
                        building.CanBuild = true;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed && building.CanBuild)
                    {
                        building.CanBuild = true;
                        BuildABuilding(building, pos);
                    }
                    
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
        }

        private void BuildABuilding(BuildingModel building, Vector2 position)
        {
            if (building.ResourceType == ResourceType.Population)
            {
                // TODO: Population increase implementation
                _playerResourceModel.IncreasePopulationLimit(building.Type == BuildingType.House ? 5 : 10);
            }

            foreach (var c in building.Cost)
            {
                _playerResourceModel.AddResource(c.Type, -c.Amount);
            }
            _clickCooldown = _clickCooldownPeriod;
            building.IsSelected = false;

            var b = new BuildingModel(building.Frame, building.Width, building.Height, building.Type, building.ResourceGenCooldown, building.ResourceType, building.ResourceAmount, building.Cost, building.UpkeepCost)
            {
                Position = position,
                Area = new Rectangle((int)position.X, (int)position.Y, building.Width, building.Height)
            };
            _buildingListModel.BuiltBuildings.Add(b);
            _playerResourceModel.AddResourceCost(b.UpkeepCost);
        }

        private void BuiltBuildings(GameTime gameTime)
        {
            foreach(var building in _buildingListModel.BuiltBuildings)
            {
                building.ResourceCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (building.ResourceCooldown > building.ResourceGenCooldown)
                {
                    _playerResourceModel.AddResource(building.ResourceType, building.ResourceAmount);
                    _playerResourceModel.SubtractResources(building.UpkeepCost.Type);
                    building.ResourceCooldown = 0;
                }
            }
        }

        private bool IntersectsAnotherBuilding(Rectangle area)
        {
            foreach(var building in _buildingListModel.BuiltBuildings)
            {
                if (building.Area.Intersects(area))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
