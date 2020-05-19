using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Interfaces;
using GenericCityBuilderRPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GenericCityBuilderRPG.Controllers
{
    class BackgroundController : IController
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerResourcesModel _playerResourcesModel;
        private TerrainTileListModel _terrainTileListModel;
        private float _harvesterCooldown = 0f;
        private float _rightClickCooldownPeriod = 200f;
        private float _rightClickCooldown = 0f;
        public BackgroundController(PlayerModel playerModel, TerrainTileListModel terrainListModel, PlayerResourcesModel playerResources)
        {
            _playerModel = playerModel;
            _terrainTileListModel = terrainListModel;
            _playerResourcesModel = playerResources;
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_harvesterCooldown > 0f)
            {
                _harvesterCooldown -= deltaTime;
                return;
            }

            _harvesterCooldown = 0f;
            var mouseState = Mouse.GetState();
            var mousePoint = mouseState.Position;
            var mousePosition = VirtualScreenSize.ScreenToWorld(_playerModel.Position, mousePoint.X, mousePoint.Y);
            var distance = Vector2.Distance(mousePosition, _playerModel.Position);
            if (mouseState.LeftButton == ButtonState.Pressed && distance <= _playerModel.ResourceHarvester.Range)
            {
                foreach (var resource in _terrainTileListModel.Resources)
                {
                    if (resource.Area.Contains(mousePosition) && resource.Amount > 0)
                    {
                        _harvesterCooldown = _playerModel.ResourceHarvester.Cooldown;
                        resource.Amount -= _playerModel.ResourceHarvester.Speed;
                        _playerResourcesModel.AddResource(resource.Type, _playerModel.ResourceHarvester.Speed);
                        break;
                    }
                }
            }

            if (_rightClickCooldown > 0f)
            {
                _rightClickCooldown -= deltaTime;
                return;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                foreach (var resource in _terrainTileListModel.Resources)
                {
                    if (resource.Area.Contains(mousePosition) && resource.Amount > 0)
                    {
                        _rightClickCooldown = _rightClickCooldownPeriod;
                        resource.AmountVisible = !resource.AmountVisible;
                        break;
                    }
                }
            }
        }
    }
}
