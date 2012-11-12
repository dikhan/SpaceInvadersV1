using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Fireball : Sprite
    {

        const int MAX_DISTANCE = 500;

        public bool Visible = false;

        Vector2 startPosition;
        Vector2 speed;
        Vector2 direction;

        public void Fire(Vector2 startPosition, Vector2 speed, Vector2 direction)
        {
            Position = startPosition;
            this.startPosition = startPosition;
            this.speed = speed;
            this.direction = direction;
            Visible = true;
        }

        public void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager, "sprites/Fireball");
            Scale = 0.3f;
        }

        public void Update(GameTime theGameTime)
        {
            if (Vector2.Distance(startPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
            }

            if (Visible == true)
            {
                base.Update(theGameTime, speed, direction);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible == true)
            {
                base.Draw(spriteBatch);
            }
        }

    }
}
