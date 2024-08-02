using GeometryWars_EvaHautecler.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Interface
{
    public interface ILaserManager
    {
        void AddLasers(Texture2D laserTexture, Rectangle laserRectangle, Vector2 direction);
        void RemoveLaser(SpaceshipLaser laser);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        List<SpaceshipLaser> GetLasers();
    }
}
