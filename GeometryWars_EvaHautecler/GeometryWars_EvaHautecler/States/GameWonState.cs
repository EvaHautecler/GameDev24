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
        private SpriteFont font;
        private string gameWonMessage = "You won! Press W to Return to Main Menu";

        public GameWonState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            font = game.Content.Load<SpriteFont>("File");
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
            game.SpriteBatch.Begin();
            var messageSize = font.MeasureString(gameWonMessage);
            game.SpriteBatch.DrawString(font, gameWonMessage, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.Green);
            game.SpriteBatch.End();
        }
    }
}
