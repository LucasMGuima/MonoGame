using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ZeldaLike;

namespace ZeldaLike.Assets
{
    internal class GameContent
    {
        //Texturas
        public Texture2D imgHuman { get; set; }
        public Texture2D imgElf { get; set; }
        public Texture2D imgOrc { get; set; }

        public GameContent(ContentManager content)
        {
            //Carregar as imagens
            imgHuman = content.Load<Texture2D>("Characters/PNG/Human/human_regular_hair");
            imgElf = content.Load<Texture2D>("Characters/PNG/Elf/elf_regular_hair");
            imgOrc = content.Load<Texture2D>("Characters/PNG/Orc/orc_regular_hair");
        }
    }
}
