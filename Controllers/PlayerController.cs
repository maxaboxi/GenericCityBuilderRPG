using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Interfaces;
using GenericCityBuilderRPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace GenericCityBuilderRPG.Controllers
{
    /// <summary>
    /// Player controller. Handles player input and updates the player model.
    /// </summary>
    class PlayerController : IController
    {
        private readonly PlayerModel _playerModel;
        private readonly TerrainTileListModel _tiles;
        private static readonly float MoveCooldownPeriod = 0.2f;
        private PlayerAnimationController _animation = null;

        public event EventHandler MoveFinished;
        public bool Disabled { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="playerModel">Player model</param>
        public PlayerController(PlayerModel playerModel, TerrainTileListModel tiles)
        {
            _playerModel = playerModel;
            _tiles = tiles;
        }

        /// <summary>
        /// Update the player. Takes input from the keyboard and updates the position of the player.
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            Rectangle visibleArea = VirtualScreenSize.CalculateVisibleArea(_playerModel.Position);
            //var visibleWaterTiles = _tiles.Tiles.Where(t => t.Area.Intersects(visibleArea) && t.Type == BiomeType.Water).ToList();
            var deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();

            if (_animation != null && !_animation.Done)
            {
                if (pressedKeys.Length == 0)
                {
                    _animation = null;
                    return;
                }
                _animation.Update(deltaTime);
                _playerModel.Position = _animation.Position + new Vector2(deltaTime, 0);
                _playerModel.Frame = _animation.CurrentFrame;
                return;
            }

            if (_animation != null && _animation.Done)
            {

                _playerModel.Position = _animation.Position;
                _playerModel.Frame = _animation.CurrentFrame;
                MoveFinished?.Invoke(this, EventArgs.Empty);
            }

            if (!Disabled)
            {
                _animation = null;
                if (pressedKeys.Contains(Keys.W))
                {
                    _animation = new PlayerAnimationController(new int[] { 8, 9, 10, 11, 12, 13, 14, 15 },
                                                   _playerModel.Position,
                                                   new Vector2(0, -_playerModel.Speed),
                                                   MoveCooldownPeriod);
                }
                else if (pressedKeys.Contains(Keys.A))
                {
                    _animation = new PlayerAnimationController(new int[] { 16, 17, 18, 19, 20, 21, 22, 23 },
                                                   _playerModel.Position,
                                                   new Vector2(-_playerModel.Speed, 0),
                                                   MoveCooldownPeriod);
                }
                else if (pressedKeys.Contains(Keys.S))
                {
                    _animation = new PlayerAnimationController(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 },
                                                   _playerModel.Position,
                                                   new Vector2(0, _playerModel.Speed),
                                                   MoveCooldownPeriod);
                }
                else if (pressedKeys.Contains(Keys.D))
                {
                    _animation = new PlayerAnimationController(new int[] { 24, 25, 26, 27, 28, 29, 30, 31 },
                                                   _playerModel.Position,
                                                   new Vector2(_playerModel.Speed, 0),
                                                   MoveCooldownPeriod);
                }
            }

           
        }
    }
}
