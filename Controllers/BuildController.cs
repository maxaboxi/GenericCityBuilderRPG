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
        private float _resourceCooldown = 0f;
        private float _resourceGenCooldown = 30000f;

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
                    if (IntersectsAnotherBuilding(new Rectangle((int)pos.X - 10, (int)pos.Y - 10, building.Width - 30, building.Height - 30)))
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

            var b = new BuildingModel(building.Frame, building.Width, building.Height, building.Type, building.ResourceType, building.ResourceAmount, building.Cost, building.UpkeepCost)
            {
                Position = position,
                Area = new Rectangle((int)position.X, (int)position.Y, building.Width - 30, building.Height - 30)
            };
            _buildingListModel.BuiltBuildings.Add(b);
            _playerResourceModel.AddUpkeepCost(b.UpkeepCost);
        }

        private void BuiltBuildings(GameTime gameTime)
        {
            _resourceCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_resourceCooldown > _resourceGenCooldown && _buildingListModel.BuiltBuildings.Count > 0)
            {
                foreach (var building in _buildingListModel.BuiltBuildings)
                {
                    if (building.ResourceType != ResourceType.Population)
                    {
                        _playerResourceModel.AddResource(building.ResourceType, building.ResourceAmount);
                    }
                    else
                    {
                        if (_playerResourceModel.Food > 0 && _playerResourceModel.Population < _playerResourceModel.PopulationLimit)
                        {
                            _playerResourceModel.AddResource(ResourceType.Population, 1);
                        }
                    }
                }
                _playerResourceModel.SubtractResources();
                _resourceCooldown = 0f;
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
