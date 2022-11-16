using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bricks
{
    internal class Paddle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float ScreenWidth { get; set; }

        private Texture2D imgPaddle { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        public Paddle(float x, float y, float screenWidth, SpriteBatch spriteBatch, GameContent gameContent)
        {
            X = x;
            Y = y;
            imgPaddle = gameContent.imgPaddle;
            Width = imgPaddle.Width;
            Height = imgPaddle.Height;
            this.spriteBatch = spriteBatch;
            ScreenWidth = screenWidth;
        }

        public void Draw()
        {
            spriteBatch.Draw(this.imgPaddle, 
                            new Vector2(this.X, this.Y), 
                            null, 
                            Color.White, 0, new Vector2(0, 0), 
                            1.0f, SpriteEffects.None, 0);
        }

        public void MoveLeft()
        {
            X = X - 5;
            if(X < 1)
            {
                X = 1;
            }
        }
        public void MoveRight()
        {
            X = X + 5;
            if ((X + Width) > ScreenWidth)
            {
                X = ScreenWidth - Width;
            }
        }

        public void MoveTo(float X)
        {
            if(X > 0)
            {
                if(X < ScreenWidth - Width)
                {
                    this.X = X;
                }
                else
                {
                    this.X = ScreenWidth - Width;
                }
            }
            else
            {
                if (X < 0)
                {
                    this.X = 0;
                }
            }
        }
    }
}
