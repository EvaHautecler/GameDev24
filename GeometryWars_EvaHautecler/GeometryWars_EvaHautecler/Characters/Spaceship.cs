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

        public Spaceship(Texture2D spaceshipTexture, KeyboardReader keyboardReader)
        {
            this.spaceshipTexture = spaceshipTexture;
            this.keyboardReader = keyboardReader;
            spaceshipRectangle = new Rectangle(100, 100, 70, 70);
        }

        public void Update(GameTime gameTime)
        {
            spaceshipRectangle = keyboardReader.ReadInput(spaceshipRectangle, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spaceshipTexture, spaceshipRectangle, Color.White);
        }
    }
}
