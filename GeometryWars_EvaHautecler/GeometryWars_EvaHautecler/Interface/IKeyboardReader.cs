using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Interface
{
    public interface IKeyboardReader
    {
        float CalculateAngle();
        Rectangle ReadInput(Rectangle spacehipRectangle, GameTime gameTime);
        bool IsShooting();
    }
}
