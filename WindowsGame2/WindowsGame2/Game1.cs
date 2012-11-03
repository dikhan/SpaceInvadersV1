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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Boolean changeDir = false;
        Boolean goDown = false;

        Enemy enemy;
        Enemy enemy2;

        Fighter fighter;

        List<Bullet> bullets = new List<Bullet>();

        Sprite mBackgroundOne;
        Sprite mBackgroundTwo;
        Sprite mBackgroundThree;
        Sprite mBackgroundFour;
        Sprite mBackgroundFive;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            fighter = new Fighter();

            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            enemy = new Enemy(new Sprite(Content.Load<Texture2D>("sprites/addexisting"), new Vector2(500, 0)), 10);
            enemy2 = new Enemy(new Sprite(Content.Load<Texture2D>("sprites/addexisting"), new Vector2(200.0f, 50)), 25);

            // Loading the background
            mBackgroundOne = new Sprite(Content.Load<Texture2D>("background/Background01"), new Vector2(0, 0), 1.05f);
            mBackgroundTwo = new Sprite(Content.Load<Texture2D>("background/Background02"), new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 0), 1.05f);
            mBackgroundThree = new Sprite(Content.Load<Texture2D>("background/Background03"), new Vector2(mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width, 0), 1.05f);
            mBackgroundFour = new Sprite(Content.Load<Texture2D>("background/Background04"), new Vector2(mBackgroundThree.Position.X + mBackgroundThree.Size.Width, 0), 1.05f);
            mBackgroundFive = new Sprite(Content.Load<Texture2D>("background/Background05"), new Vector2(mBackgroundFour.Position.X + mBackgroundFour.Size.Width, 0), 1.05f);

            // TODO: use this.Content to load your game content here
            fighter.LoadContent(this.Content);   

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Q))
                this.Exit();

            /* # Pixels movements each time the key is pushed */
            int m = 10;
                
            goDown = false;

            /*  Zona para que no se salgan los objetos de la pantalla */

            if ((enemy.sprite.Position.X + enemy.sprite.Size.Width) > graphics.PreferredBackBufferWidth)
            {
                changeDir = true;
                goDown = true;
            }

            if (enemy.sprite.Position.X < 1)
            {
                changeDir = false;
                goDown = true;
            }

            /*  Zona para que enemigo uno rebote cuando se choque con el */

            if (((enemy.sprite.Position.X + enemy.sprite.Size.Width) > enemy2.sprite.Position.X && enemy.sprite.Position.X < (enemy2.sprite.Position.X + enemy2.sprite.Size.Width)) &&
                 ((enemy.sprite.Position.Y + enemy.sprite.Size.Height) > enemy2.sprite.Position.Y && enemy.sprite.Position.Y < (enemy2.sprite.Position.Y + enemy2.sprite.Size.Height))
                )
            {
                Console.WriteLine("Datos de Enemy1:");
                Console.WriteLine("enemy.Location.X: " + enemy.sprite.Position.X);
                Console.WriteLine("enemy.Location.Y: " + enemy.sprite.Position.Y);

                Console.WriteLine("Datos de Enemy2:");
                Console.WriteLine("enemy2.Location.X: " + enemy2.sprite.Position.X);
                Console.WriteLine("enemy2.Location.Y: " + enemy2.sprite.Position.Y);

                changeDir = !changeDir;
                goDown = true;

            }


            int v = 400;

            if (changeDir)
                //enemy.sprite.Position = new Vector2(enemy.sprite.Position.X - v, enemy.sprite.Position.Y);
                enemy.sprite.Update(gameTime, new Vector2(v,1), new Vector2(-1,0));
            else
                //enemy.sprite.Position = new Vector2(enemy.sprite.Position.X + v, enemy.sprite.Position.Y);
                enemy.sprite.Update(gameTime, new Vector2(v, 1), new Vector2(1, 0));

            if (goDown)
                enemy.sprite.Update(gameTime, new Vector2(1, v), new Vector2(1, 1));
                //enemy.sprite.Position = new Vector2(enemy.sprite.Position.X, enemy.sprite.Position.Y + v);

            //if (showBullet)
            if(bullets.Count != 0)
                foreach(Bullet bullet in bullets)
                    bullet.sprite.Position = new Vector2(bullet.sprite.Position.X, bullet.sprite.Position.Y - bullet.Velocity);




            if (mBackgroundOne.Position.X < -mBackgroundOne.Size.Width)
            {
                mBackgroundOne.Position.X = mBackgroundFive.Position.X + mBackgroundFive.Size.Width;
            }

            if (mBackgroundTwo.Position.X < -mBackgroundTwo.Size.Width)
            {
                mBackgroundTwo.Position.X = mBackgroundOne.Position.X + mBackgroundOne.Size.Width;
            }

            if (mBackgroundThree.Position.X < -mBackgroundThree.Size.Width)
            {
                mBackgroundThree.Position.X = mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width;
            }

            if (mBackgroundFour.Position.X < -mBackgroundFour.Size.Width)
            {
                mBackgroundFour.Position.X = mBackgroundThree.Position.X + mBackgroundThree.Size.Width;
            }

            if (mBackgroundFive.Position.X < -mBackgroundFive.Size.Width)
            {
                mBackgroundFive.Position.X = mBackgroundFour.Position.X + mBackgroundFour.Size.Width;
            }

            Vector2 aDirection = new Vector2(-1, 0);
            Vector2 aSpeed = new Vector2(160, 0);

            mBackgroundOne.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundTwo.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundThree.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundFour.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundFive.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            fighter.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSalmon);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Sprites are drawn in layer so the backgound must be drown first
            mBackgroundOne.Draw(this.spriteBatch);
            mBackgroundTwo.Draw(this.spriteBatch);
            mBackgroundThree.Draw(this.spriteBatch);
            mBackgroundFour.Draw(this.spriteBatch);
            mBackgroundFive.Draw(this.spriteBatch);

            enemy.sprite.Draw(spriteBatch, Color.Red);
            enemy2.sprite.Draw(spriteBatch, Color.White);

            fighter.Draw(this.spriteBatch);

            if (bullets.Count != 0)
                foreach (Bullet bullet in bullets)
                    bullet.sprite.Draw(spriteBatch, Color.Khaki);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
