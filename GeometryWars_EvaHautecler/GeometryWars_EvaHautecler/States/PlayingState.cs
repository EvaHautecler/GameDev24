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
        private Texture2D level1EnemyTexture;
        private Texture2D level2EnemyTexture;
        private Texture2D level3EnemyTexture;
        private Texture2D heartTexture;
        private Texture2D healthBarTexture;

        private Spaceship spaceship;
        private Boss boss;
        private bool bossDefeated;
        private List<Enemy> enemies;
        private Random random;
        private float enemySpawnCooldown = 2.5f;
        private float enemySpawnTimer;
        private int enemiesSpawned;
        private KeyboardReader keyboardReader;
        private LaserManager laserManager;

        private bool isGameOver;
        private int score;
        private int currentLevel;
        private int[] levelThresholds = { 70, 120, 150, 200 };
        private SpriteFont font;

        private bool transitioning;
        private float transitionTimer;
        private const float TransitionDelay = 2f;

        private int lives;
        private const int maxLives = 3;

        public PlayingState(Game1 game, int initialLevel = 1)
        {
            this.game = game;
            isGameOver = false;
            score = 0;
            currentLevel = initialLevel;
            transitioning = false;
            transitionTimer = 0;

            lives = initialLevel == 4 ? maxLives : 1;
        }

        public void Enter()
        {
            backgroundTexture = game.Content.Load<Texture2D>("Background");
            spaceshipTexture = game.Content.Load<Texture2D>("Spaceship");
            spaceshipLaserTexture = game.Content.Load<Texture2D>("Charge");
            level1EnemyTexture = game.Content.Load<Texture2D>("Enemy1");
            level2EnemyTexture = game.Content.Load<Texture2D>("Enemy2");
            level3EnemyTexture = game.Content.Load<Texture2D>("Enemy3");
            healthBarTexture = game.Content.Load<Texture2D>("HealthBar");
            heartTexture = game.Content.Load<Texture2D>("Heart");
            font = game.Content.Load<SpriteFont>("File");

            keyboardReader = new KeyboardReader();
            laserManager = new LaserManager();
            spaceship = new Spaceship(spaceshipTexture, spaceshipLaserTexture, keyboardReader);
            random = new Random();
            enemies = new List<Enemy>();
            boss = new Boss(level3EnemyTexture, 200f, random, healthBarTexture);
        }

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

            if (transitioning)
            {
                transitionTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (transitionTimer <= 0)
                {
                    game.ChangeState(new LevelTransitionState(game, currentLevel));
                }
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
            if (currentLevel < 5)
            {
                if (enemySpawnTimer <= 0 )
                {
                    SpawnEnemiesForLevel();
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
                            score += enemy.PointValue;
                            break;
                        }
                    }

                    if (enemy.GetRectangle().Intersects(spaceship.GetCollisionRectangle()) && !spaceship.IsInvulnerable())
                    {
                        HandlePlayerHit();
                        break;
                    }

                    foreach (var enemies in enemies)
                    {
                        enemy.HandleCollision(enemies);

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
            }
            else if(currentLevel == 5)
            {
                boss.Update(gameTime, new Vector2(spaceship.Rectangle.Center.X, spaceship.Rectangle.Center.Y));
                foreach (var laser in spaceship.GetLaserManager().GetLasers())
                {
                    if (boss.GetRectangle().Intersects(laser.LaserRectangle()))
                    {
                        boss.TakeDamage();
                        spaceship.GetLaserManager().RemoveLaser(laser);
                        if (boss.IsDefeated)
                        {
                            bossDefeated = true;
                            score += boss.PointValue;
                        }
                        break;
                    }
                }

                if (boss.GetRectangle().Intersects(spaceship.GetCollisionRectangle()))
                {
                    game.ChangeState(new GameOverState(game));
                }
            }
             CheckLevelProgression();
        }

        public void Draw(GameTime gameTime)
        {
            game.SpriteBatch.Begin();
            game.SpriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 2000, 988), Color.White);


            spaceship.Draw(game.SpriteBatch);
            laserManager.Draw(game.SpriteBatch);

            if (currentLevel < 5)
            {
                foreach (var enemy in enemies)
                {
                    enemy.Draw(game.SpriteBatch);
                }
            }
            else
            {
                boss.Draw(game.SpriteBatch);
                boss.DrawHealthBar(game.SpriteBatch);
            }

            
            game.SpriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 10), Color.White);
            if (currentLevel == 4 || currentLevel == 5)
            {
                for (int i = 0; i < lives; i++)
                {
                    game.SpriteBatch.Draw(heartTexture, new Vector2(10 + i * 40, 50), Color.White);
                }
            }
            if (transitioning)
            {
                game.ChangeState(new LevelTransitionState(game, currentLevel ));
            }
            game.SpriteBatch.End();
        }

        private void HandlePlayerHit()
        {
            if (lives > 1)
            {
                lives--;
                spaceship.ActivateInvulnerability();
            }
            else
            {
                isGameOver = true;
            }
        }
        private void SpawnEnemiesForLevel()
        {
            int enemyCount = 1 + currentLevel;
            for (int i = 0; i < enemyCount ; i++)
            {
                int numberEnemiesToSpawn = (currentLevel == 1 && i == 0) ? 10 : 5;
                switch (currentLevel)
                {
                    case 1:
                        
                        enemies.Add(new Enemy(level1EnemyTexture, 130f, random, 5, EnemyType.Type1));
                        enemiesSpawned++;
                        
                        break;
                    case 2:
                        enemies.Add(new Enemy(level2EnemyTexture, 100f, random, 10,EnemyType.Type2));
                        enemiesSpawned++;
                        break;
                    case 3:
                        enemies.Add(new Enemy(level3EnemyTexture, 100f, random, 15, EnemyType.Type3));
                        enemiesSpawned++;
                        break;
                    case 4:
                        enemies.Add(new Enemy(level1EnemyTexture, 100f, random, 5, EnemyType.Type1));
                        enemies.Add(new Enemy(level2EnemyTexture, 100f, random, 10, EnemyType.Type2));
                        enemies.Add(new Enemy(level3EnemyTexture, 100f, random, 15, EnemyType.Type3));
                        enemiesSpawned += 3;
                        break;
                    case 5:
                        enemies.Add(boss);
                        break;
                }
            }
        }

        private void CheckLevelProgression()
        {
            if (currentLevel < 4 && score >= levelThresholds[currentLevel - 1])
            {
                enemies.Clear();
                score = 0;
                currentLevel++;
                enemiesSpawned = 0;
                transitioning = true;
                transitionTimer = TransitionDelay;
            }
            else if (currentLevel == 4 && score >= levelThresholds[3])
            {
                enemies.Clear();
                score = 0;
                currentLevel++;
                bossDefeated = false;
                transitioning = true;
                transitionTimer = TransitionDelay;
            }
            else if (currentLevel == 5 && bossDefeated)
            {
                game.ChangeState(new GameWonState(game));
            }
        }
    }
}
