using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Display
{
    public class ScoreDisplay : IScoreObserver
    {
        private SpriteFont font;
        private Vector2 position;
        private int currentScore;

        public ScoreDisplay(SpriteFont font, Vector2 position)
        {
            this.font = font;
            this.position = position;
        }

        public void UpdateScore(int newScore)
        {
            currentScore = newScore;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"Score: {currentScore}", position, Color.White);
        }
    }
}
