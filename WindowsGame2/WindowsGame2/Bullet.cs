using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Bullet : Base
    {

        private Vector2 location;
        public Vector2 Location
        {
            get
            {
                if (this.location == null)
                    this.location = new Vector2(0,50);

                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public int Velocity { get; set; }
        public Texture2D Texture { get; set; }

        public Bullet(Vector2 location, Texture2D texture, int velocity)
        {
            this.Location = location;
            this.Texture = texture;
            this.Velocity = velocity;
        }

        public Bullet(Sprite sprite, int velocity)
        {
            this.sprite = sprite;
            this.Velocity = velocity;
        }

    }
}
