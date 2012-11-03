using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Fighter : Base
    {

        public int Health { get; set; }

        public Fighter(Vector2 location,int health, Texture2D texture)
        {
            this.Location = location;
            this.Texture = texture;
            this.Health = health;
        }

        public Fighter(Sprite sprite, int health)
        {
            this.sprite = sprite;
            this.Health = health;
        }

    }
}
