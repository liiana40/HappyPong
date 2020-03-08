using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
   public class Paddle
    {
        public Rectangle Position = new Rectangle(0, 0, 32, 96);
        public int Speed = 6;
        Keys Up, Down;
        //bool isLeft;
    public Paddle(int x)
    {
        Position.X = x;
        Position.Y = 288;
        if (x < 472)
        {
            Up = Keys.W;
            Down = Keys.S;
        }
        else
            {
                Up = Keys.Up;
                Down = Keys.Down;
            }
    }
    public void  Update(KeyboardState ks)
    {
        if (ks.IsKeyDown(Up) && (Position.Y > Speed))
            {
                Position.Y -= Speed;
            }
        if (ks.IsKeyDown(Down) && (Position.Bottom + Speed < 480))
            {
                Position.Y += Speed;
            }
        }
    public void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.Tex, Position, Color.White);
        }
    }
}
