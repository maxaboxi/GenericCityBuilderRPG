using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using GenericCityBuilderRPG.FSM;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Interfaces;
using GenericCityBuilderRPG.Views;
using System.Collections.Generic;
using System;
using GenericCityBuilderRPG.Controllers;
using GenericCityBuilderRPG.Models;
using GenericCityBuilderRPG.Factories;
using System.Linq;
using GenericLooterShooterRPG.Views;

namespace GenericCityBuilderRPG.States
{
    class GameState : BaseState
    {
        private readonly List<BaseView> _views = new List<BaseView>();
        private readonly List<IController> _controllers = new List<IController>();
        private PlayerModel _playerModel;
        private TerrainTileListModel _terrainTileListModel;
        private PlayerResourcesModel _playerResourcesModel;
        private SpriteBatch _spriteBatch;
        private Camera _camera;
        private RenderTarget2D _screen;
        private List<Song> _bgMusic;

        public GameState(StateMachine stateMachine) : base(stateMachine)
        {
            _camera = new Camera();
            _bgMusic = new List<Song>
            {
                stateMachine.Game.Content.Load<Song>("level01"),
                stateMachine.Game.Content.Load<Song>("level02"),
                stateMachine.Game.Content.Load<Song>("level03"),
                stateMachine.Game.Content.Load<Song>("level04")
            };
        }
        public override void Draw()
        {
            StateMachine.Game.GraphicsDevice.SetRenderTarget(_screen);
            StateMachine.Game.GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            foreach(var view in _views)
            {
                view.Draw();
            }

            _spriteBatch.End();

            StateMachine.Game.GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_screen, new Rectangle(0, 0, VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier), Color.White);

            _spriteBatch.End();

        }

        public override void Enter(params object[] args)
        {
            _screen = new RenderTarget2D(StateMachine.Game.GraphicsDevice, VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier);
            _spriteBatch = new SpriteBatch(StateMachine.Game.GraphicsDevice);       

            bool resetForNewGame = true;
            if (args.Length > 0 && args[0] is bool)
            {
                resetForNewGame = (bool)args[0];
            }

            // Create models
            _playerModel = new PlayerModel();
            _playerResourcesModel = new PlayerResourcesModel();
            var tiles = TerrainFactory.GenerateTiles().OrderBy(x => Guid.NewGuid()).ToList();
            var resources = TerrainFactory.GenerateResources(tiles);
            _terrainTileListModel = new TerrainTileListModel(tiles, resources);

            // Create and add controllers
            var playerController = new PlayerController(_playerModel, _terrainTileListModel);
            var backgroundController = new BackgroundController(_playerModel, _terrainTileListModel, _playerResourcesModel);

            _controllers.Add(playerController);
            _controllers.Add(backgroundController);

            // Add views
            _views.Add(new BackgroundView(StateMachine.Game.Content, _spriteBatch, _terrainTileListModel, _playerModel));
            _views.Add(new PlayerResourcesView(StateMachine.Game.Content, _spriteBatch, _playerResourcesModel, _playerModel));
            _views.Add(new PlayerView(StateMachine.Game.Content, _spriteBatch, _playerModel));
        }

        public override void Exit()
        {
            _views.Clear();
            _controllers.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.PlayPosition.TotalSeconds == 0.0f)
            {
                Random rand = new Random();
                MediaPlayer.Play(_bgMusic[rand.Next(_bgMusic.Count)]);
                MediaPlayer.Volume = 0.2f;
            }

            foreach (var controller in _controllers)
            {
                _camera.Follow(_playerModel);
                controller.Update(gameTime);
            }
        }

    }
}
