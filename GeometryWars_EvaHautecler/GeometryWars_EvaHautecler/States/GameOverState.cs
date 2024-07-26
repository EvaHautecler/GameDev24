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
        private SpriteFont font;
        private string gameOverMessage = "Game Over! Press M to Return to Main Menu";

        public GameOverState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            font = game.Content.Load<SpriteFont>("File");
        }

        public void Exit()
        {
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
            var messageSize = font.MeasureString(gameOverMessage);
            game.SpriteBatch.DrawString(font, gameOverMessage, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.Red);
            game.SpriteBatch.End();
        }
    }
}
