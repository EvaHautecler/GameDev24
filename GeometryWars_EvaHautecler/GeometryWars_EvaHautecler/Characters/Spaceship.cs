using Game_Development_Space_Shooter.Animaties;
using GeometryWars_EvaHautecler.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public Spaceship(Texture2D spaceshipTexture, KeyboardReader keyboardReader)
        {
            this.spaceshipTexture = spaceshipTexture;
            this.keyboardReader = keyboardReader;
            spaceshipRectangle = new Rectangle(100, 100, 70, 70);

            animation = new Animation();
            animation.GetFramesFromTextureProperties(spaceshipTexture.Width, spaceshipTexture.Height, 1, 1);
        }

        public void Update(GameTime gameTime)
        {
            spaceshipRectangle = keyboardReader.ReadInput(spaceshipRectangle, gameTime);
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(spaceshipTexture, spaceshipRectangle, Color.White);
            spriteBatch.Draw(spaceshipTexture, spaceshipRectangle,animation.CurrentFrame.SourceRectangle, Color.White, keyboardReader.CalculateAngle(), new Vector2(spaceshipTexture.Width/2, spaceshipTexture.Height/2), SpriteEffects.None, 0f);
        }
    }
}
