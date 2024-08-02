using GeometryWars_EvaHautecler.Characters;
using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Manager
{
    public class LaserManager : ILaserManager
    {
        private List<SpaceshipLaser> lasers = new List<SpaceshipLaser>();
        

        public void AddLasers(Texture2D laserTexture, Rectangle laserRectangle, Vector2 direction)
        {
            SpaceshipLaser laser = new SpaceshipLaser(laserTexture, laserRectangle, direction);
            lasers.Add(laser);
        }

        public void RemoveLaser(SpaceshipLaser laser)
        {
            lasers.Remove(laser);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                lasers[i].Update();
                if (!IsLaserOnScreen(lasers[i].LaserRectangle()))
                {
                    lasers.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (SpaceshipLaser laser in lasers)
            {
                laser.Draw(spriteBatch);
            }
        }

        public List<SpaceshipLaser> GetLasers()
        {
            return lasers;
        }

        private bool IsLaserOnScreen(Rectangle laserRectangle)
        {
            return laserRectangle.Right >= 0 && laserRectangle.Left <= 2000 && laserRectangle.Bottom >= 0 && laserRectangle.Top <= 988;
        }
    }
}
