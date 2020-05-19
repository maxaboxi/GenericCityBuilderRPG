using GenericCityBuilderRPG.Components;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MessageBox = System.Windows.Forms.MessageBox;

namespace GenericCityBuilderRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GenericCityBuilderRPG : Game
    {
        public GraphicsDeviceManager graphics;

        public GenericCityBuilderRPG()
        {
            IsMouseVisible = true;
            var width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            var fullScreen = true; // change to true
            if(width < 1920 || height < 1080)
            {
                Exit();
                using (StreamWriter w = File.AppendText("error.log"))
                {
                    w.WriteLine(DateTime.Now.ToString() + ": At least 1920 x 1080 resolution is required");
                }
                MessageBox.Show("At least 1920 x 1080 resolution is required", "Unsupported resolution", MessageBoxButtons.OK);
            }

            if (width > 1920 && width < 3840)
            {
                width = 1920;
                fullScreen = false;
            }

            if (height > 1080 && height < 2160)
            {
                height = 1080;
                fullScreen = false;
            }

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = width, // change to width
                PreferredBackBufferHeight = height, // change to height
                SynchronizeWithVerticalRetrace = false,
                IsFullScreen = fullScreen
            };
            Content.RootDirectory = "Content";
            VirtualScreenSize.ScreenSizeMultiplier = width / VirtualScreenSize.Width; // change 1920 to width
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var stateComponent = new StateComponent(this);
            var gameState = new GameState(stateComponent.StateMachine);
            stateComponent.StateMachine.Add("game", gameState);

            // Start the game by entering the 'menu' state
            stateComponent.StateMachine.Change("game", true);
            Components.Add(stateComponent);
            base.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
