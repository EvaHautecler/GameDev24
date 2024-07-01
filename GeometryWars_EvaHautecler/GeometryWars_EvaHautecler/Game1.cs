using GeometryWars_EvaHautecler.Characters;
using GeometryWars_EvaHautecler.Input;
using GeometryWars_EvaHautecler.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GeometryWars_EvaHautecler
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D backgroundTexture;
        private Texture2D spaceshipTexture;
        private Texture2D spaceshipLaserTexture;

        private Spaceship spaceship;
        private KeyboardReader keyboardReader;
        private LaserManager laserManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 2000;
            _graphics.PreferredBackBufferHeight = 988;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            keyboardReader = new KeyboardReader();
            laserManager = new LaserManager();
            spaceship = new Spaceship(spaceshipTexture,spaceshipLaserTexture, keyboardReader);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("Background");
            spaceshipTexture = Content.Load<Texture2D>("Spaceship");
            spaceshipLaserTexture = Content.Load<Texture2D>("Charge");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            spaceship.Update(gameTime);
            laserManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 2000, 988), Color.White);
            spaceship.Draw(_spriteBatch);
            laserManager.Draw(_spriteBatch);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}