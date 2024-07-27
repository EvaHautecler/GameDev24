using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Characters
{
    public enum EnemyType { Type1, Type2, Type3}
    public class Enemy
    {
        private Texture2D enemyTexture;
        private Rectangle enemyRectangle;
        private float speed;
        private Random random;
        private EnemyType enemyType;
        private Vector2 direction;
        private Vector2 zigzagDirection;
        private float zigzagTimer;
        private const float zigzagInterval = 0.5f;
        public int PointValue { get; private set; }

        public Enemy(Texture2D enemyTexture, float speed, Random random, int pointValue, EnemyType enemyType)
        {
            this.enemyTexture = enemyTexture;
            this.speed = speed;
            //enemy1Rectangle = new Rectangle((int)initialPosition.X, (int)initialPosition.Y, 70, 70);
            this.random = random;
            this.PointValue = pointValue;
            this.enemyType = enemyType;

            SpawnOutsideScreen();

            if (enemyType == EnemyType.Type1)
            {
                SetRandomDirection();
            }
            else if (enemyType == EnemyType.Type2)
            {
                SetZigzagDirection();
                zigzagTimer = zigzagInterval;
            }
        }

        public void SpawnOutsideScreen()
        {
            int screenWidth = 2000;
            int screenHeight = 988;
            int positionChoice = random.Next(4);

            int x = 0, y = 0;

            switch (positionChoice)
            {
                case 0:
                    x = random.Next(screenWidth);
                    y = -enemyTexture.Height;
                    break;
                case 1:
                    x = random.Next(screenWidth);
                    y = screenHeight;
                    break;
                case 2: // Left
                    x = -enemyTexture.Width;
                    y = random.Next(screenHeight);
                    break;
                case 3: // Right
                    x = screenWidth;
                    y = random.Next(screenHeight);
                    break;
            }

            enemyRectangle = new Rectangle(x, y, 70, 70);
        }

        private void SetRandomDirection()
        {
            direction = new Vector2((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1));
            direction.Normalize();
        }
        private void SetZigzagDirection()
        {
            zigzagDirection = new Vector2((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1));
            zigzagDirection.Normalize();
        }

        public void Update(GameTime gameTime, Vector2 heroPosition)
        {

            Vector2 enemyPosition = new Vector2(enemyRectangle.X, enemyRectangle.Y);

            if (enemyType == EnemyType.Type2)
            {
                zigzagTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (zigzagTimer <= 0)
                {
                    SetZigzagDirection();
                    zigzagTimer = zigzagInterval;
                }
                enemyPosition += zigzagDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 directionToHero = heroPosition - enemyPosition;
                directionToHero.Normalize();
                enemyPosition += directionToHero * speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
            }
            else if ( enemyType == EnemyType.Type3)
            {
                Vector2 directionToHero = heroPosition - enemyPosition;
                directionToHero.Normalize();
                enemyPosition += directionToHero * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (enemyType == EnemyType.Type1)
            {
                enemyPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (enemyPosition.X < 0 || enemyPosition.X > 2000 - enemyRectangle.Width)
                {
                    direction.X = -direction.X;
                }
                if (enemyPosition.Y < 0 || enemyPosition.Y > 988 - enemyRectangle.Height)
                {
                    direction.Y = -direction.Y;
                }
            }

            enemyRectangle.X = (int)enemyPosition.X;
            enemyRectangle.Y = (int)enemyPosition.Y;

            /*Vector2 enemyPosition = new Vector2(enemyRectangle.X, enemyRectangle.Y);
            Vector2 direction = heroPosition - enemyPosition;
            direction.Normalize();

            enemyPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            enemyRectangle.X = (int)enemyPosition.X;
            enemyRectangle.Y = (int)enemyPosition.Y;*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, enemyRectangle, Color.White);
        }

        public Rectangle GetCollisionRectangle()
        {
            int padding = 10;
            return new Rectangle(
                enemyRectangle.X + padding,
                enemyRectangle.Y + padding,
                enemyRectangle.Width - 2 * padding,
                enemyRectangle.Height - 2 * padding);
        }

        public Rectangle GetRectangle()
        {
            return enemyRectangle;
        }
    }
}
