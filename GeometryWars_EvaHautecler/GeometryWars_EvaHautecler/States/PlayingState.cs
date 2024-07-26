using GeometryWars_EvaHautecler.Characters;
using GeometryWars_EvaHautecler.Input;
using GeometryWars_EvaHautecler.Interface;
using GeometryWars_EvaHautecler.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.States
{
    public class PlayingState : IGameState
    {
        private Game1 game;
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
        private bool isGameOver;

        public PlayingState(Game1 game)
        {
            this.game = game;
            isGameOver = false;
        }

        public void Enter()
        {
            backgroundTexture = game.Content.Load<Texture2D>("Background");
            spaceshipTexture = game.Content.Load<Texture2D>("Spaceship");
            spaceshipLaserTexture = game.Content.Load<Texture2D>("Charge");
            enemy1Texture = game.Content.Load<Texture2D>("Enemy1");

            keyboardReader = new KeyboardReader();
            laserManager = new LaserManager();
            spaceship = new Spaceship(spaceshipTexture, spaceshipLaserTexture, keyboardReader);
            random = new Random();
            enemies = new List<Enemy>();
        }

        public void Exit() { }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.ChangeState(new MainMenuState(game));
            }

            if (isGameOver)
            {
                game.ChangeState(new GameOverState(game));
                return;
            }

            spaceship.Update(gameTime);
            laserManager.Update(gameTime);
            enemy1SpawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enemy1SpawnTimer <= 0)
            {
                enemies.Add(new Enemy(enemy1Texture, 100f, random));
                enemy1SpawnTimer = enemy1SpawnCooldown;
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime, new Vector2(spaceship.Rectangle.Center.X, spaceship.Rectangle.Center.Y));

                foreach (var laser in spaceship.GetLaserManager().GetLasers())
                {
                    if (enemies[i].GetRectangle().Intersects(laser.LaserRectangle()))
                    {
                        enemies.RemoveAt(i);
                        break;
                    }
                }

                if (enemies[i].GetRectangle().Intersects(spaceship.Rectangle))
                {
                    isGameOver = true;
                    //game.ChangeState(new GameOverState(game));
                    break;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.SpriteBatch.Begin();
            game.SpriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 2000, 988), Color.White);

            /*if (isGameOver)
            {
                var font = game.Content.Load<SpriteFont>("File");
                var message = "Game Over";
                var messageSize = font.MeasureString(message);
                game.SpriteBatch.DrawString(font, message, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.Red);
            }
            else
            {
            }*/

                spaceship.Draw(game.SpriteBatch);
                laserManager.Draw(game.SpriteBatch);

                foreach (var enemy in enemies)
                {
                   enemy.Draw(game.SpriteBatch);
                }
            game.SpriteBatch.End();
        }
    }
}
