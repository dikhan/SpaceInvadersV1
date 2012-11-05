using System;
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
        /* Constants - General for all the Sprites */
        public const int MOVE_UP = -1;
        public const int MOVE_DOWN = 1;
        public const int MOVE_LEFT = -1;
        public const int MOVE_RIGHT = 1;

        //The asset name for the Sprite's Texture
        public string AssetName;

        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D spriteTexture;

        //The size of the Sprite
        public Rectangle Size;

        //Used to size the Sprite up or down from the original image
        public float scale = 2.0f;
        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(source.Width * Scale), (int)(source.Height * Scale));
            }
        }

        //The Rectangular area from the original image that 
        //defines the Sprite. 
        Rectangle source;
        public Rectangle Source
        {
            get { return source; }
            set
            {
                source = value;
                Size = new Rectangle(0, 0, (int)(source.Width * Scale), (int)(source.Height * Scale));
            }
        }

        public Sprite() { }

        public Sprite(Texture2D spriteTexture, Vector2 position)
        {
            this.spriteTexture = spriteTexture;
            this.Position = position;
            source = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
            this.Scale = scale;
        }

        public Sprite(Texture2D spriteTexture, Vector2 position, float scale)
        {
            this.spriteTexture = spriteTexture;
            this.Position = position;
            source = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
            this.Scale = scale;
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager contentManager, string assetName)
        {
            spriteTexture = contentManager.Load<Texture2D>(assetName);
            AssetName = assetName;
            source = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
            Size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
        }

        /* Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        // gameTime: It´s used to keep how fast the sprite moves consistent across different computers
        // speed: How fast the sprite will move
        // direction: Indicates if something should be moving to the left/right or up/down
        */
        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Position, Source, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(spriteTexture, Position, Source, color, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}
