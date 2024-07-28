using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Characters
{
    public class Boss : Enemy
    {
        private int health;
        private const int maxHealth = 15;
        private Texture2D healthBarTexture;
        private Vector2 healthBarPosition;

        public Boss(Texture2D enemyTexture, float speed, Random random, Texture2D healthBarTexture): base(enemyTexture, speed, random, 15, EnemyType.Type3)
        {
            this.health = maxHealth;
            this.healthBarTexture = healthBarTexture;
            this.healthBarPosition = new Vector2(1000, 50);
        }

        public bool IsDefeated => health <= 0;

        public void TakeDamage()
        {
            health--;
        }

        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            float healthPercentage = (float)health / maxHealth;
            Rectangle healthBarRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, (int)(healthBarTexture.Width * healthPercentage), (healthBarTexture.Height/2));
            spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.Red);
        }
    }
}
