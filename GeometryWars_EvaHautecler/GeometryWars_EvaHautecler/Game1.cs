using GeometryWars_EvaHautecler.Characters;
using GeometryWars_EvaHautecler.Input;
using GeometryWars_EvaHautecler.Interface;
using GeometryWars_EvaHautecler.Manager;
using GeometryWars_EvaHautecler.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GeometryWars_EvaHautecler
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public SpriteBatch SpriteBatch { get; private set; }
        private IGameState currentState;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 2000;
            _graphics.PreferredBackBufferHeight = 988;
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            ChangeState(new MainMenuState(this));
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState?.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentState?.Draw(gameTime);
            base.Draw(gameTime);
        }

        public void ChangeState(IGameState newState)
        {
            currentState = newState;
            currentState.Enter();
        }
    }
}