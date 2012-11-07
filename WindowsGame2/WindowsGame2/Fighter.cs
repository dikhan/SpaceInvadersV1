using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Fighter : Sprite
    {

        const string WIZARD_ASSETNAME = "sprites/dummyBoy";

        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        
        const int WIZARD_SPEED = 250;
        const int JUMP_HEIGHT = 150;

        const int SECONDS_CYCLE = 2;
        const int SPRITES_CYCLE = 16;

        const int SPRITE_HEIGHT = 90;
        const int SPRITE_WIDTH = 61;

        const int STATIC_SPRITE_IP_X = 21; // Initial Point X
        const int STATIC_SPRITE_IP_Y = 16; // Initial Point Y

        enum State
        {
            Static,
            Walking,
            Jumping,
            Ducking
        }
        State currentState = State.Static;
        Vector2 startingPosition = Vector2.Zero;

        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;

        KeyboardState previousKeyboardState;

        int timeStatic = 0;

        //StatusFrames [] statusFrames = new StatusFrames [Enum.GetValues(Type.GetType("Status")).Length];
        StatusFrames[] statusFrames = new StatusFrames[4];

        public void Initialize()
        {

            StatusFrames staticFrames = new StatusFrames(1);
            staticFrames.frames = new Rectangle[] {new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT)
            
            };

            StatusFrames walkingFrames = new StatusFrames(16);
            walkingFrames.frames = new Rectangle[] {new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 1), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 2), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 3), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 4), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 5), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 6), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 7), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),                               
          
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 1), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 2), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 3), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 4), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 5), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 6), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 7), STATIC_SPRITE_IP_Y + SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT)
            
            };

            StatusFrames jumpingFrames = new StatusFrames(2);
            jumpingFrames.frames = new Rectangle[] {new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT)
            
            };

            StatusFrames duckingFrames = new StatusFrames(2);
            duckingFrames.frames = new Rectangle[] {new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT),
                                                   new Rectangle(STATIC_SPRITE_IP_X + (SPRITE_WIDTH * 0), STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT)
            
            };

            statusFrames[(int)State.Static] = staticFrames;
            statusFrames[(int)State.Walking] = walkingFrames;
            statusFrames[(int)State.Jumping] = jumpingFrames;
            statusFrames[(int)State.Ducking] = duckingFrames;

        }

        public void LoadContent(ContentManager contentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(contentManager, WIZARD_ASSETNAME);
            Source = new Rectangle(134,6, 35, 35);
            Initialize();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateStatic(currentKeyboardState, gameTime);
            UpdateWalking(currentKeyboardState);
            UpdateJump(currentKeyboardState);
            UpdateDuck(currentKeyboardState);

            UpdateSprite(gameTime);

            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime, speed, direction);
        }

        private void UpdateStatic(KeyboardState currentKeyboardState, GameTime gameTime)
        {
            Keys[] keys = currentKeyboardState.GetPressedKeys();

            if (currentState == State.Static && keys.Length == 0)
            {
                
            }
        }

        private void UpdateWalking(KeyboardState currentKeyboardState)
        {
            if (currentState == State.Walking || currentState == State.Static)
            {
                StopMoving();
                speed = new Vector2(WIZARD_SPEED, WIZARD_SPEED);
                Boolean walking = UpdateDirectionsWithKeyboardState(currentKeyboardState, true, true);
                if (walking)
                    currentState = State.Walking;
                else
                    currentState = State.Static;
            }
        }

        private void UpdateJump(KeyboardState currentKeyboardState)
        {
            if (currentState == State.Walking || currentState == State.Static)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Space) == true && previousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (currentState == State.Jumping)
            {
                if (startingPosition.Y - Position.Y > JUMP_HEIGHT) // Para saber la altura del salto, el limite cuando debe empezar a cae
                {
                    direction.Y = MOVE_DOWN;
                }

                if (Position.Y > startingPosition.Y) // To know when the sprite has arrived, touch land
                {
                    Position.Y = startingPosition.Y;
                    currentState = State.Static;
                    StopMoving();
                }

                UpdateDirectionsWithKeyboardState(currentKeyboardState, true, false);
            }
        }

        private void UpdateDuck(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.D) == true)
            {
                if (currentState == State.Walking || currentState == State.Static)
                {
                    StopMoving();
		            Source = new Rectangle(200, 0, 200, Source.Height);
		            currentState = State.Ducking;
                }
            }else
            {
                if (currentState == State.Ducking)
                {
                    Source = new Rectangle(0, 0, 200, Source.Height);
                    currentState = State.Static;
                }
            }
        }

        private void UpdateSprite(GameTime gameTime)
        {
            // preparar el cambio
            // sumar el numero de pixels
            // total segundos para completar el ciclo = const int 2
            // numero de sprites en el ciclo = const int 6
            // numero para sumar y pasar el siguiente sprite const int 35

            // Starting pixel in X = 134

            timeStatic += gameTime.ElapsedGameTime.Milliseconds;
            if (timeStatic > (SECONDS_CYCLE * 1000 / SPRITES_CYCLE))
            {
                Source = statusFrames[(int)currentState].getFrame();
                timeStatic = 0;
            }
            //currentState = State.Static;

        }

        private void Jump()
        {
            if (currentState != State.Jumping)
            {
                currentState = State.Jumping;
                startingPosition = Position;
                direction.Y = MOVE_UP;
                speed = new Vector2(WIZARD_SPEED, WIZARD_SPEED);
            }

        }


        private Boolean UpdateDirectionsWithKeyboardState(KeyboardState currentKeyboardState, Boolean updateX, Boolean updateY)
        {
            if (updateX)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    direction.X = MOVE_LEFT;
                    flipHorizontal = true;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    direction.X = MOVE_RIGHT;
                    flipHorizontal = false;
                }

                /*if (currentKeyboardState.IsKeyDown(Keys.Left) == true || currentKeyboardState.IsKeyDown(Keys.Right) == true)
                    currentState = State.Walking;*/
            }

            if (updateY)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    direction.Y = MOVE_UP;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    direction.Y = MOVE_DOWN;
                }

                /*if (currentKeyboardState.IsKeyDown(Keys.Up) == true || currentKeyboardState.IsKeyDown(Keys.Down) == true)
                    currentState = State.Walking;*/
            }

            return (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.Down));

        }

        private void StopMoving()
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;
        }

        /*public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Position, Source, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }*/

    }
}