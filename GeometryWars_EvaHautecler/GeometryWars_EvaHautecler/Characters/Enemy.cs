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
        private Texture2D enemy1Texture;
        private Rectangle enemy1Rectangle;
        private float speed;
        private Random random;

        public Enemy(Texture2D enemy1Texture, float speed, Random random)
        {
            this.enemy1Texture = enemy1Texture;
            this.speed = speed;
            //enemy1Rectangle = new Rectangle((int)initialPosition.X, (int)initialPosition.Y, 70, 70);
            this.random = random;

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
                    y = -enemy1Texture.Height;
                    break;
                case 1:
                    x = random.Next(screenWidth);
                    y = screenHeight;
                    break;
                case 2: // Left
                    x = -enemy1Texture.Width;
                    y = random.Next(screenHeight);
                    break;
                case 3: // Right
                    x = screenWidth;
                    y = random.Next(screenHeight);
                    break;
            }

            enemy1Rectangle = new Rectangle(x, y, 70, 70);
        }

        public void Update(GameTime gameTime, Vector2 heroPosition)
        {
            Vector2 enemy1Position = new Vector2(enemy1Rectangle.X, enemy1Rectangle.Y);
            Vector2 direction = heroPosition - enemy1Position;
            direction.Normalize();

            enemy1Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            enemy1Rectangle.X = (int)enemy1Position.X;
            enemy1Rectangle.Y = (int)enemy1Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemy1Texture, enemy1Rectangle, Color.White);
        }
    }
}
