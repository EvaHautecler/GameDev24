﻿using GeometryWars_EvaHautecler.Characters;
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
        private Texture2D level1EnemyTexture;
        private Texture2D level2EnemyTexture;
        private Texture2D level3EnemyTexture;
        private Texture2D level4EnemyTexture;

        private Spaceship spaceship;
        private List<Enemy> enemies;
        private Random random;
        private float enemySpawnCooldown = 4f;
        private float enemySpawnTimer;
        private KeyboardReader keyboardReader;
        private LaserManager laserManager;
        private bool isGameOver;
        private int score;
        private int currentLevel;
        private int[] levelThresholds = { 50, 100, 150, 200 };
        private SpriteFont font;

        public PlayingState(Game1 game)
        {
            this.game = game;
            isGameOver = false;
            score = 0;
            currentLevel = 1;
        }

        public void Enter()
        {
            backgroundTexture = game.Content.Load<Texture2D>("Background");
            spaceshipTexture = game.Content.Load<Texture2D>("Spaceship");
            spaceshipLaserTexture = game.Content.Load<Texture2D>("Charge");
            level1EnemyTexture = game.Content.Load<Texture2D>("Enemy1");
            level2EnemyTexture = game.Content.Load<Texture2D>("Enemy2");
            level3EnemyTexture = game.Content.Load<Texture2D>("Enemy3");
            font = game.Content.Load<SpriteFont>("File");

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

            if (score >= levelThresholds[3])
            {
                game.ChangeState(new GameWonState(game));
                return;
            }

            spaceship.Update(gameTime);
            laserManager.Update(gameTime);
            enemySpawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enemySpawnTimer <= 0)
            {
                SpawnEnemiesForLevel();

                //enemies.Add(new Enemy(level1EnemyTexture, 100f, random));
                enemySpawnTimer = enemySpawnCooldown;
            }

            List<Enemy> enemiesToRemove = new List<Enemy>();
            List<SpaceshipLaser> lasersToRemove = new List<SpaceshipLaser>();

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, new Vector2(spaceship.Rectangle.Center.X, spaceship.Rectangle.Center.Y));

                foreach (var laser in spaceship.GetLaserManager().GetLasers())
                {
                    if (enemy.GetRectangle().Intersects(laser.LaserRectangle()))
                    {
                        enemiesToRemove.Add(enemy);
                        lasersToRemove.Add(laser);
                        score += 5;
                        break;
                    }
                }

                if (enemy.GetRectangle().Intersects(spaceship.GetCollisionRectangle()))
                {
                    isGameOver = true;
                    break;
                }
            }

            foreach (var laser in lasersToRemove)
            {
                spaceship.GetLaserManager().RemoveLaser(laser);
            }

            foreach (var enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
            }

            CheckLevelProgression();
        }

        public void Draw(GameTime gameTime)
        {
            game.SpriteBatch.Begin();
            game.SpriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 2000, 988), Color.White);


                spaceship.Draw(game.SpriteBatch);
                laserManager.Draw(game.SpriteBatch);

                foreach (var enemy in enemies)
                {
                   enemy.Draw(game.SpriteBatch);
                }
            game.SpriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 10), Color.White);
            game.SpriteBatch.End();
        }

        private void SpawnEnemiesForLevel()
        {
            int enemyCount = 1 + currentLevel;
            for (int i = 0; i < enemyCount; i++)
            {
                switch (currentLevel)
                {
                    case 1:
                        enemies.Add(new Level1Enemy(level1EnemyTexture, 100f, random));
                        break;
                    case 2:
                        enemies.Add(new Level2Enemy(level2EnemyTexture, 120f, random));
                        break;
                    case 3:
                        enemies.Add(new Level3Enemy(level3EnemyTexture, 140f, random));
                        break;
                    case 4:
                        enemies.Add(new Level1Enemy(level1EnemyTexture, 100f, random));
                        enemies.Add(new Level2Enemy(level2EnemyTexture, 120f, random));
                        enemies.Add(new Level3Enemy(level3EnemyTexture, 140f, random));
                        break;
                }
            }
        }

        private void CheckLevelProgression()
        {
            if (score >= levelThresholds[2] && currentLevel < 4)
            {
                currentLevel = 4;
            }
            else if (score >= levelThresholds[1] && currentLevel < 3)
            {
                currentLevel = 3;
            }
            else if (score >= levelThresholds[0] && currentLevel < 2)
            {
                currentLevel = 2;
            }
        }
    }
}
