using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Characters
{
    public class Level2Enemy : Enemy
    {
        public Level2Enemy(Texture2D enemyTexture, float speed, Random random, int pointValue) : base(enemyTexture, speed, random, pointValue) { }
    }
}
