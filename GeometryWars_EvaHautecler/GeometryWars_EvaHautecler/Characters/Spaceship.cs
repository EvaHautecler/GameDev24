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

        public Spaceship(Texture2D spaceshipTexture)
        {
            this.spaceshipTexture = spaceshipTexture;
            spaceshipRectangle = new Rectangle(100, 100, 70, 70);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spaceshipTexture, spaceshipRectangle, Color.White);
        }
    }
}
