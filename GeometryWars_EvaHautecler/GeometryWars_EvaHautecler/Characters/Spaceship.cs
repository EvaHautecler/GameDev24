using Game_Development_Space_Shooter.Animaties;
using GeometryWars_EvaHautecler.Input;
using GeometryWars_EvaHautecler.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Characters
{
    public class Spaceship
    {
        private Texture2D spaceshipTexture;
        private Rectangle spaceshipRectangle;
        private KeyboardReader keyboardReader;
        private Animation animation;

        private Texture2D laserTexture;
        private LaserManager laserManager;
        private float laserCooldown = 0.25f;
        private float laserTimer;

        public Spaceship(Texture2D spaceshipTexture, Texture2D laserTexture, KeyboardReader keyboardReader)
        {
            this.spaceshipTexture = spaceshipTexture;
            this.laserTexture = laserTexture;
            this.keyboardReader = keyboardReader;
            spaceshipRectangle = new Rectangle(100, 100, 70, 70);

            animation = new Animation();
            animation.GetFramesFromTextureProperties(spaceshipTexture.Width, spaceshipTexture.Height, 1, 1);

            laserManager = new LaserManager();
            laserTimer = laserCooldown;
        }

        public void Update(GameTime gameTime)
        {
            spaceshipRectangle = keyboardReader.ReadInput(spaceshipRectangle, gameTime);
            animation.Update(gameTime);

            laserTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (laserTimer <= 0)
            {
                Shoot();
                laserTimer = laserCooldown;
            }

            laserManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(spaceshipTexture, spaceshipRectangle, Color.White);
            spriteBatch.Draw(spaceshipTexture, spaceshipRectangle,animation.CurrentFrame.SourceRectangle, Color.White, keyboardReader.CalculateAngle(), new Vector2(spaceshipTexture.Width/2, spaceshipTexture.Height/2), SpriteEffects.None, 0f);
            laserManager.Draw(spriteBatch);
        }

        private void Shoot()
        {
            float angle = keyboardReader.CalculateAngle();
            Vector2 direction = new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
            Vector2 startPosition = new Vector2(spaceshipRectangle.Center.X, spaceshipRectangle.Center.Y);
            startPosition -= new Vector2(spaceshipRectangle.Width / 2, spaceshipRectangle.Height / 2);

            // Initialize the laser with the appropriate height and width
            Rectangle laserRectangle = new Rectangle((int)startPosition.X, (int)startPosition.Y, 10, 5);  // Adjust height as needed
            laserManager.AddLasers(laserTexture, laserRectangle, direction);
        }

        public Rectangle Rectangle => spaceshipRectangle;
        
    }
}
