using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GenericCityBuilderRPG.Views
{
    class PlayerView : BaseView
    {
        private readonly SpriteSheet _player;
        private readonly PlayerModel _playerModel;

        // Uncomment for debugging
        //private readonly SpriteBatch _spriteBatch;
        //private readonly SpriteFont _gameFont;
        public PlayerView(ContentManager contentManager, SpriteBatch spriteBatch, PlayerModel playerModel) : base(contentManager, spriteBatch)
        {
            var allCharTextures = contentManager.Load<Texture2D>("player");
            _playerModel = playerModel;
            _player = new SpriteSheet(spriteBatch, allCharTextures, _playerModel.Width, _playerModel.Height);

            // Uncomment for debugging
            //_spriteBatch = spriteBatch;
            //_gameFont = contentManager.Load<SpriteFont>("FontFile");
        }


        public override void Draw()
        {
            _player.Draw(_playerModel.Position, _playerModel.Frame, Color.White, _playerModel.Scale);

            //Uncomment for debugging
           //_spriteBatch.DrawString(_gameFont, _playerModel.Position.ToString().Replace("{", "").Replace("}", ""), _playerModel.Position + new Vector2(0, 50), Color.Yellow);
        }
    }
}
