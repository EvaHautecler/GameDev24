using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Characters
{
    public class Level1Enemy : Enemy
    {
        public Level1Enemy(Texture2D enemyTexture, float speed, Random random) : base(enemyTexture, speed, random) { }
    }
}
