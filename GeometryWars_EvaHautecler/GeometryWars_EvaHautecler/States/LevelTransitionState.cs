using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
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
            font = game.Content.Load<SpriteFont>("GL-Nummernschild");
        }

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
            var messageSize = font.MeasureString($"YOU ARE NOW BEGINNING LEVEL {nextLevel}");
            if (nextLevel == 5)
            {
                game.SpriteBatch.DrawString(font, $"YOU ARE NOW BEGINNING THE FINAL BOSS LEVEL", new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.White);
            }
            else
            {
                game.SpriteBatch.DrawString(font, $"YOU ARE NOW BEGINNING LEVEL {nextLevel}", new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.White);
            }
            game.SpriteBatch.End();
        }
    }
}
