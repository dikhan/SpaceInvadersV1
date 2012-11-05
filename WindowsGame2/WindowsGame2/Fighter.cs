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

        const string WIZARD_ASSETNAME = "sprites/soldier";

        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        
        const int WIZARD_SPEED = 250;
        const int JUMP_HEIGHT = 100;

        const int SECONDS_CYCLE = 2;
        const int SPRITES_CYCLE = 9; //6

        const int SPRITE_HEIGHT = 46; //35
        const int SPRITE_WIDTH = 36;

        const int STATIC_SPRITE_IP_X = 26; // Initial Point X - Static Sprite 134
        const int STATIC_SPRITE_IP_Y = 43; // Initial Point Y - Static Sprite 6

        int spritePosition = 1;
        int previousX = STATIC_SPRITE_IP_X;

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

        public void LoadContent(ContentManager contentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(contentManager, WIZARD_ASSETNAME);
            Source = new Rectangle(134,6, 35, 35);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateStatic(currentKeyboardState, gameTime);
            UpdateWalking(currentKeyboardState);
            UpdateJump(currentKeyboardState);
            UpdateDuck(currentKeyboardState);

            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime, speed, direction);
        }

        private void UpdateStatic(KeyboardState currentKeyboardState, GameTime gameTime)
        {
            Keys[] keys = currentKeyboardState.GetPressedKeys();

            if (currentState == State.Static && keys.Length == 0)
            {
                timeStatic += gameTime.ElapsedGameTime.Milliseconds;
                // preparar el cambio
                // sumar el numero de pixels
                // total segundos para completar el ciclo = const int 2
                // numero de sprites en el ciclo = const int 6
                // numero para sumar y pasar el siguiente sprite const int 35

                // Starting pixel in X = 134

                if (timeStatic > (SECONDS_CYCLE * 1000 / SPRITES_CYCLE))
                {
                    spritePosition += 1;
                    if (spritePosition > SPRITES_CYCLE)
                    {
                        previousX = STATIC_SPRITE_IP_X;
                        spritePosition = 1;
                    }
                    else
                        previousX += SPRITE_WIDTH;

                    Source = new Rectangle(previousX, STATIC_SPRITE_IP_Y, SPRITE_WIDTH, SPRITE_HEIGHT);
                    timeStatic = 0;
                }
            }
            else
            {
                timeStatic = 0;
                previousX = STATIC_SPRITE_IP_X;
            }
        }


        private void UpdateWalking(KeyboardState currentKeyboardState)
        {
            if (currentState == State.Walking || currentState == State.Static)
            {
                StopMoving();
                speed = new Vector2(WIZARD_SPEED, WIZARD_SPEED);
                UpdateDirectionsWithKeyboardState(currentKeyboardState, true, true);
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


        private void UpdateDirectionsWithKeyboardState(KeyboardState currentKeyboardState, Boolean updateX, Boolean updateY)
        {
            if (updateX)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    direction.X = MOVE_LEFT;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    direction.X = MOVE_RIGHT;
                }
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
            }
        }

        private void StopMoving()
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Position, Source, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}