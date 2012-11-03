using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsGame2
{
    class Enemy : Base
    {

        public int Health { get; set; }

        public Enemy(Vector2 location,int health, Texture2D texture)
        {
            this.Location = location;
            this.Health = health;
            this.Texture = texture;
        }

        public Enemy(Sprite sprite, int health)
        {
            this.sprite = sprite;
            this.Health = health;
        }

    }
}
