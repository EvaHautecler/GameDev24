using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Characters
{
    public class Enemy
    {
        private Texture2D enemyTexture;
        private Rectangle enemyRectangle;
        private float speed;
        private Random random;
        public int PointValue { get; private set; }

        public Enemy(Texture2D enemyTexture, float speed, Random random, int pointValue)
        {
            this.enemyTexture = enemyTexture;
            this.speed = speed;
            //enemy1Rectangle = new Rectangle((int)initialPosition.X, (int)initialPosition.Y, 70, 70);
            this.random = random;
            this.PointValue = pointValue;

            SpawnOutsideScreen();
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

        public void Update(GameTime gameTime, Vector2 heroPosition)
        {
            Vector2 enemyPosition = new Vector2(enemyRectangle.X, enemyRectangle.Y);
            Vector2 direction = heroPosition - enemyPosition;
            direction.Normalize();

            enemyPosition += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            enemyRectangle.X = (int)enemyPosition.X;
            enemyRectangle.Y = (int)enemyPosition.Y;
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
