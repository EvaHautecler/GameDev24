using GeometryWars_EvaHautecler.Interface;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;

namespace GeometryWars_EvaHautecler.States
{
    public class MainMenuState : IGameState
    {
        private Game1 game;
        private SpriteFont fontMain;
        private string message = "Press Enter to Start";

        public MainMenuState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            fontMain = game.Content.Load<SpriteFont>("GomePixelFont");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                game.ChangeState(new PlayingState(game)); 
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.SpriteBatch.Begin();
            var messageSize = fontMain.MeasureString(message);
            game.SpriteBatch.DrawString(fontMain, message, new Vector2((game.GraphicsDevice.Viewport.Width - messageSize.X) / 2, (game.GraphicsDevice.Viewport.Height - messageSize.Y) / 2), Color.White);
            game.SpriteBatch.End();
        }
    }
}
