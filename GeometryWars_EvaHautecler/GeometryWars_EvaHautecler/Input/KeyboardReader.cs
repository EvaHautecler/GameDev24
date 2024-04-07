using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryWars_EvaHautecler.Input
{
    public class KeyboardReader
    {
        //private Rectangle spaceshipRectangle;
        private float spaceshipSpeed = 2.0f;

        private Dictionary<(Keys, Keys), float> keyCombinations = new Dictionary<(Keys, Keys), float>
        {
            {(Keys.Down, Keys.None), MathHelper.ToRadians(0f) },
            {(Keys.Left, Keys.None), MathHelper.ToRadians(90f) },
            {(Keys.Right,Keys.None), MathHelper.ToRadians(270f) },
            {(Keys.Up, Keys.None), MathHelper.ToRadians(180f) },
            {(Keys.Right, Keys.Down), MathHelper.ToRadians(315f) },
            {(Keys.Right, Keys.Up), MathHelper.ToRadians(225f) },
            {(Keys.Left, Keys.Down), MathHelper.ToRadians(45f) },
            {(Keys.Left, Keys.Up), MathHelper.ToRadians(135f) }
        };

        public float CalculateAngle()
        {
            float angle = 0f;
            foreach (var combination in keyCombinations)
            {
                if (Keyboard.GetState().IsKeyDown(combination.Key.Item1) && Keyboard.GetState().IsKeyDown(combination.Key.Item2)) 
                {
                    angle = combination.Value;
                    break;
                }
                else if (Keyboard.GetState().IsKeyDown(combination.Key.Item1) && combination.Key.Item2 == Keys.None)
                {
                    angle = combination.Value;
                }
            }
            return angle;
        }

        public Rectangle ReadInput(Rectangle spaceshipRectangle, GameTime gameTime)
        {
            Rectangle newSpaceship = new Rectangle(spaceshipRectangle.X, spaceshipRectangle.Y, spaceshipRectangle.Width, spaceshipRectangle.Height);
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                newSpaceship.X -= (int)(spaceshipSpeed * gameTime.ElapsedGameTime.TotalSeconds * 60);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                newSpaceship.X += (int)(spaceshipSpeed * gameTime.ElapsedGameTime.TotalSeconds * 60);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                newSpaceship.Y -= (int)(spaceshipSpeed * gameTime.ElapsedGameTime.TotalSeconds * 60);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                newSpaceship.Y += (int)(spaceshipSpeed * gameTime.ElapsedGameTime.TotalSeconds * 60);
            }

            return newSpaceship;
        }

    }
}
