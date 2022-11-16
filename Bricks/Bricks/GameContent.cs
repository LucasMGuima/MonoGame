using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bricks
{
    internal class GameContent
    {
        //Texturas 
        public Texture2D imgBrick { get; set; }
        public Texture2D imgPaddle { get; set; }
        public Texture2D imgBall { get; set; }
        public Texture2D imgPixel { get; set; }

        //Sons
        public SoundEffect startSound { get; set; }
        public SoundEffect brickSound { get; set; }
        public SoundEffect paddleBounceSound { get; set; }
        public SoundEffect wallBounceSound { get; set; }
        public SoundEffect missSound { get; set; }

        //Fonte
        public SpriteFont labelFont { get; set; }

        public GameContent(ContentManager content)
        {
            //Carrega imagens
            imgBall = content.Load<Texture2D>("Assets/ball");
            imgBrick = content.Load<Texture2D>("Assets/brick");
            imgPaddle = content.Load<Texture2D>("Assets/Paddle");
            imgPixel = content.Load<Texture2D>("Assets/pixel");
            //Carrega sons
            startSound = content.Load<SoundEffect>("Assets/StartSound");
            brickSound = content.Load<SoundEffect>("Assets/BrickSound");
            paddleBounceSound = content.Load<SoundEffect>("Assets/PaddleBounceSound");
            wallBounceSound = content.Load<SoundEffect>("Assets/WallBounceSound");
            missSound = content.Load<SoundEffect>("Assets/MissSound");
            //Carrega fontes
            labelFont = content.Load<SpriteFont>("Assets/Arial20");
        }
    }
}
