using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.States
{
    public class LevelTransitionState : IGameState
    {
        private Game1 game;
        private SpriteFont font;
        private int nextLevel;
        private double timer;

        public LevelTransitionState(Game1 game, int nextLevel)
        {
            this.game = game;
            this.nextLevel = nextLevel;
            this.timer = 2.0;
        }

        public void Enter()
        {
            font = game.Content.Load<SpriteFont>("File");
        }

        public void Exit() { }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0)
            {
                game.ChangeState(new PlayingState(game, nextLevel));
            }
        }

        public void Draw( GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.SpriteBatch.Begin();
            game.SpriteBatch.DrawString(font, $"You are now beginning level {nextLevel}", new Vector2(100, 100), Color.White);
            game.SpriteBatch.End();
        }
    }
}
