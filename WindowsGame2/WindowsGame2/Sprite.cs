﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame2
{
    class Sprite
    {

        //The asset name for the Sprite's Texture
        public string AssetName;

        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D spriteTexture;

        //The size of the Sprite
        public Rectangle Size;

        //Used to size the Sprite up or down from the original image
        public float scale = 1.0f;
        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
            }
        }

        public Sprite() { }

        public Sprite(Texture2D spriteTexture, Vector2 position)
        {
            this.spriteTexture = spriteTexture;
            this.Position = position;
            Size = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
        }

        public Sprite(Texture2D spriteTexture, Vector2 position, float scale)
        {
            this.spriteTexture = spriteTexture;
            this.Position = position;
            this.Scale = scale;
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager contentManager, string assetName)
        {
            spriteTexture = contentManager.Load<Texture2D>(assetName);
            AssetName = assetName;
            Size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            // gameTime is used to keep how fast the sprite moves consistent across different computers
            // speed is how fast the sprite will move
            // direction indicates if something should be moving to the left/right or up/down
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Position, new Rectangle(0, 0, (int)(spriteTexture.Width), (int)(spriteTexture.Height)),
                             Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            /*spriteBatch.Draw(this.spriteTexture, this.Position, new Rectangle(0, 0, (int)(spriteTexture.Width * this.Scale), (int)(spriteTexture.Height * this.Scale)),
                             color, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);*/
            spriteBatch.Draw(spriteTexture, Position, new Rectangle(0, 0, (int)(spriteTexture.Width), (int)(spriteTexture.Height)),
                             color, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}