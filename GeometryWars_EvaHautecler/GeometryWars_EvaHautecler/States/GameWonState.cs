using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.States
{
    public class GameWonState : IGameState
    {
        private Game1 game;
        private SpriteFont font1;
        private SpriteFont font2;
        private string gameWonMessage = "You won!";
        private string gameWonMessage2 = "Press W to Return to Main Menu";

        public GameWonState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            font1 = game.Content.Load<SpriteFont>("BigFont");
            font2 = game.Content.Load<SpriteFont>("File");
        }

        public void Exit() { }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                game.ChangeState(new MainMenuState(game));
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.SpriteBatch.Begin();
            var messageSize = font1.MeasureString(gameWonMessage);
            game.SpriteBatch.DrawString(font1, gameWonMessage, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.Green);
            var messageSize2 = font2.MeasureString(gameWonMessage2);
            game.SpriteBatch.DrawString(font2, gameWonMessage2, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize2.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize2.Y) / 4), Color.Green);
            game.SpriteBatch.End();
        }
    }
}
