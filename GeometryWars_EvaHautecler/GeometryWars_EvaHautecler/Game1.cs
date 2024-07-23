using GeometryWars_EvaHautecler.Characters;
using GeometryWars_EvaHautecler.Input;
using GeometryWars_EvaHautecler.Manager;
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
        private SpriteBatch _spriteBatch;
        private Texture2D backgroundTexture;
        private Texture2D spaceshipTexture;
        private Texture2D spaceshipLaserTexture;
        private Texture2D enemy1Texture;

        private Spaceship spaceship;
        private List<Enemy> enemies;
        private Random random;
        private float enemy1SpawnCooldown = 2f;
        private float enemy1SpawnTimer;
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
            random = new Random();
            enemies = new List<Enemy>();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("Background");
            spaceshipTexture = Content.Load<Texture2D>("Spaceship");
            spaceshipLaserTexture = Content.Load<Texture2D>("Charge");
            enemy1Texture = Content.Load<Texture2D>("Enemy1");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            spaceship.Update(gameTime);
            laserManager.Update(gameTime);
            enemy1SpawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enemy1SpawnTimer <= 0)
            {
                enemies.Add(new Enemy(enemy1Texture, 100f, random));
                enemy1SpawnTimer = enemy1SpawnCooldown;
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, new Vector2(spaceship.Rectangle.Center.X, spaceship.Rectangle.Center.Y));
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 2000, 988), Color.White);
            spaceship.Draw(_spriteBatch);
            laserManager.Draw(_spriteBatch);

            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}