using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Interface
{
    public interface IMovable
    {
        void Update(GameTime gameTime, Vector2 heroPosition);
    }
}
