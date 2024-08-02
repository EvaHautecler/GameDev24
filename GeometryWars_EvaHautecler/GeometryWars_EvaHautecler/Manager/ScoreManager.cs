using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpDX.DirectWrite.GdiInterop;

namespace GeometryWars_EvaHautecler.Manager
{
    public class ScoreManager
    {
        //private Game1 game;
        private int score;
        private List<IScoreObserver> observers = new List<IScoreObserver>();
        //private SpriteFont font;

        public int Score
        {
            get { return score; }
            //private
            set
            {
                score = value;
                NotifyObservers();
            }
        }
        /*public void Enter()
        {
            font = game.Content.Load<SpriteFont>("File");
        }*/

        public void AddPoints(int points)
        {
            Score += points;
        }

        public void Attach(IScoreObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IScoreObserver observer)
        {
            observers.Remove(observer);
        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 10), Color.White);
        }*/

        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.UpdateScore(score);
            }
        }
    }
}
