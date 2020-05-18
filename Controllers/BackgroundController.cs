using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Interfaces;
using GenericCityBuilderRPG.Models;
using GenericLooterShooterRPG.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GenericCityBuilderRPG.Controllers
{
    class BackgroundController : IController
    {
        private readonly PlayerModel _playerModel;
        private TerrainTileListModel _terrainTileListModel;
        private MouseState _previousMouseState;
        private MouseState _currentMouseState;
        public BackgroundController(PlayerModel playerModel, TerrainTileListModel terrainListModel)
        {
            _playerModel = playerModel;
            _terrainTileListModel = terrainListModel;
        }

        public void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            var mousePoint = _currentMouseState.Position;
            var mousePosition = VirtualScreenSize.ScreenToWorld(_playerModel.Position, mousePoint.X, mousePoint.Y);
            if (_currentMouseState.LeftButton == ButtonState.Pressed && (_previousMouseState == null || _previousMouseState.LeftButton == ButtonState.Released))
            {
                foreach (var resource in _terrainTileListModel.Resources)
                {
                    if (resource.Area.Contains(mousePosition))
                    {
                        resource.Amount -= _playerModel.ResourceHarvester.Speed;
                        PlayerResources.AddResource(resource.Type, _playerModel.ResourceHarvester.Speed);
                        break;
                    }
                }
            }
        }
    }
}
