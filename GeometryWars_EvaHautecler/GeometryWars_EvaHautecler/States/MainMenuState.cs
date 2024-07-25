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
    public class MainMenuState : IGameState
    {
        private Game1 game;
        private SpriteFont font;

        public MainMenuState(Game1 game)
        {
            this.game = game;
        }

        public void Enter()
        {
            font = game.Content.Load<SpriteFont>("Font");
        }

        public void Exit()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                //Add logic 
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
            game.SpriteBatch.Begin();
            game.SpriteBatch.DrawString(font, "Press Enter to Start", new Vector2(100, 100), Color.White);
            game.SpriteBatch.End();
        }
    }
}
