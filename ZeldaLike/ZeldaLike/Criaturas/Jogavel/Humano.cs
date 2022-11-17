using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ZeldaLike.Assets;

namespace ZeldaLike.Criaturas.Jogavel
{
    internal class Humano
    {
        private Texture2D imgHuman { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        private int posDrawX;
        private int posDrawY;

        public float X { get; set; }
        public float Y { get; set; }
        public float vel { get; set; }
        public int spriteWidth { get; set; }
        public int spriteHeight { get; set; }
        public int sheetWidth { get; set; }
        public int sheetHeight { get; set; }
        public float screenWidth { get; set; }
        public float screenHeight { get; set; }

        public Humano(float x, float y, float screenWidth, float screenHeight, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.X = x;
            this.Y = y;
            this.vel = 5;
            //Spritesheet
            this.imgHuman = gameContent.imgHuman;
            //Tamanho de cada imagem dentro da spritesheet
            this.spriteWidth = 20;
            this.spriteHeight = 20;
            //Tamanhi do sprite sheet
            this.sheetWidth = imgHuman.Width;
            this.sheetHeight = imgHuman.Height;

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.spriteBatch = spriteBatch;

            //Posicao do sprite a ser retirado da spriete sheet
            posDrawX = 0;
            posDrawY = this.spriteHeight;

        }

        public void Draw()
        {
            //Deseha o sprite correspondente da spritesheet
            if(posDrawX < spriteWidth*3)
            {
                posDrawX += this.spriteWidth;
            }
            else
            {
                posDrawX = 0;
            }

            //Retangulo do tamanho da imagem
            Rectangle rectSize = new Rectangle(this.spriteWidth, this.spriteHeight, 20, 20);
            //Retangulo que indica qual sprite desenhar da spritesheet
            Rectangle rectPos = new Rectangle(posDrawX, posDrawY, this.spriteWidth, this.spriteHeight);

            spriteBatch.Draw(imgHuman, new Vector2(X,Y), rectPos, Color.White);
        }

        #region MOVIMENTO
        public void MoveLeft()
        {
            X = X - vel;
            if(X < 1)
            {
                X = 1;
            }
        }
        public void MoveRight()
        {
            X = X + vel;
            if((X + spriteWidth) > screenWidth)
            {
                X = screenWidth - spriteWidth;
            }
        }
        public void MoveUp()
        {
            Y = Y - vel;
            if(Y < 1)
            {
                Y = 1;
            }
        }
        public void MoveDown()
        {
            Y = Y + vel;
            if((Y + spriteHeight) > screenHeight)
            {
                Y = screenHeight - spriteHeight;
            }
        }
        #endregion
    }
}
