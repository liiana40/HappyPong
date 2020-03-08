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
    public class Ball
    {
        public static Random rnd = new Random();
        public Vector2 Position = new Vector2();
        public Vector2 Velocity = new Vector2();
        public Rectangle CollisionRect = new Rectangle(396, 236, 8, 8);
        public int RightScore = 0, LeftScore = 0;

        int StartTimer = 90;
        public Ball()
        {
            Reset();
        }
        public void Reset()
        {
            StartTimer = 90;
            Position.X = 396;
            Position.Y = 236;
            do
            {
                Velocity.X = rnd.Next(-800, 800) / 100f;
            } while (Math.Abs(Velocity.X) < 2);
            do
            {
                Velocity.Y = rnd.Next(-800, 800) / 100f;
            } while (Math.Abs(Velocity.Y) < 2);
        }
        public void Update(Paddle LeftPaddle, Paddle RightPaddle)
        {
            if (StartTimer > 0)
            {
                StartTimer--;
                return;
            }
            Position += Velocity;
            if (Position.X < 0)
            {
                Game1.RightScore++;
                Game1.BallRemove.Add(this);
            }
            if (Position.X > 808)
            {
                Game1.LeftScore++;
                Game1.BallRemove.Add(this);
            }
            if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y = Math.Abs(Velocity.Y);

            }
            if (Position.Y > 472)
            {
                Position.Y = 472;
                Velocity.Y = -Math.Abs(Velocity.Y);

            }
            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;
            if (LeftPaddle.Position.Intersects(CollisionRect))
            {
                Velocity.X = Math.Abs(Velocity.X);
            }
            if (RightPaddle.Position.Intersects(CollisionRect))
            {
                Velocity.X = -Math.Abs(Velocity.X);
            }
        }
        public void Draw(SpriteBatch sb)
        {

            sb.Draw(Game1.Tex, CollisionRect, Color.White);
            // sb.DrawString(Game1.sf, "Left: " + LeftScore.ToString(), new Vector2(309, 0), Color.White); +            "\nRight: " + RightScore.ToString(), new Vector2(309, 6), Color.White);
        }
    }
}


