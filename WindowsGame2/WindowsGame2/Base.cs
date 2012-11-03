using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class Base
    {

        public Sprite sprite;

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

        public Texture2D Texture { get; set; }

        public Base() { }
        public Base(Vector2 location, Texture2D texture)
        {
            this.Location = location;
            this.Texture = texture;
        }

        public Base(Sprite sprite)
        {
            this.sprite = sprite;
        }

    }
}
