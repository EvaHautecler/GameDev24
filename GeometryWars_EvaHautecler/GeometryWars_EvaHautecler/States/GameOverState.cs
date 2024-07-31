using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GeometryWars_EvaHautecler.States
{
    public class GameOverState : IGameState
    {
        private Game1 game;
        private SpriteFont bigFont;
        private SpriteFont font;
        private string gameOverMessage = "Game Over!";
        private string gameOverMessage2 = "Press M to Return to Main Menu";

        public GameOverState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            font = game.Content.Load<SpriteFont>("File");
            bigFont = game.Content.Load<SpriteFont>("BigFont");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                game.ChangeState(new MainMenuState(game));
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.SpriteBatch.Begin();
            var messageSize1 = bigFont.MeasureString(gameOverMessage);
            game.SpriteBatch.DrawString(bigFont, gameOverMessage, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize1.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize1.Y) / 2), Color.Red);
            var messageSize2 = font.MeasureString(gameOverMessage2);
            game.SpriteBatch.DrawString(font, gameOverMessage2, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize2.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize2.Y) / 4), Color.Red);
            game.SpriteBatch.End();
        }
    }
}
