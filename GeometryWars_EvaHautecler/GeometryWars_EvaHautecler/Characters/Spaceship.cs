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

        private bool isInvulnerable;
        private float invulnerabilityTimer;
        private const float invulnerabilityDuration = 2f;

        private bool flicker;
        private float flickerTimer;
        private const float flickerInterval = 0.1f;

        public Spaceship(Texture2D spaceshipTexture, Texture2D laserTexture, KeyboardReader keyboardReader)
        {
            this.spaceshipTexture = spaceshipTexture;
            this.laserTexture = laserTexture;
            this.keyboardReader = keyboardReader;

            spaceshipRectangle = new Rectangle(1000, 494, 70, 70);

            animation = new Animation();
            animation.GetFramesFromTextureProperties(spaceshipTexture.Width, spaceshipTexture.Height, 1, 1);
            laserManager = new LaserManager();
            laserTimer = laserCooldown;
            isInvulnerable = false;
            flicker = false;
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
            if (isInvulnerable)
            {
                invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                flickerTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (flickerTimer <= 0)
                {
                    flicker = !flicker;
                    flickerTimer = flickerInterval;
                }

                if (invulnerabilityTimer <= 0)
                {
                    isInvulnerable = false;
                    flicker = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!flicker || !isInvulnerable)
            {
                if (animation.CurrentFrame != null)
                {
                    spriteBatch.Draw(spaceshipTexture, spaceshipRectangle, animation.CurrentFrame.SourceRectangle, Color.White, keyboardReader.CalculateAngle(), new Vector2(spaceshipTexture.Width / 2, spaceshipTexture.Height / 2), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(spaceshipTexture, spaceshipRectangle, Color.White);
                }
            }
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

        public Rectangle GetCollisionRectangle()
        {
            double padding = 10;
            return new Rectangle(
                spaceshipRectangle.X - (int)(3*padding),
                spaceshipRectangle.Y - (int)(3* padding),
                spaceshipRectangle.Width - (int)padding,
                spaceshipRectangle.Height - (int)padding);
        }

        public Rectangle Rectangle => spaceshipRectangle;
        
        public LaserManager GetLaserManager()
        {
            return laserManager;
        }

        public bool IsInvulnerable() { return isInvulnerable; }
        public void ActivateInvulnerability()
        {
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
            flickerTimer = flickerInterval;
        }
        
    }
}
