using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame2
{
    class Fighter : Sprite
    {

        const string WIZARD_ASSETNAME = "sprites/WizardSquare";

        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        
        const int WIZARD_SPEED = 250;
        const int JUMP_HEIGHT = 100;

        enum State
        {
            Walking,
            Jumping,
            Ducking
        }
        State currentState = State.Walking;
        Vector2 startingPosition = Vector2.Zero;

        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;

        KeyboardState previousKeyboardState;

        public void LoadContent(ContentManager contentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(contentManager, WIZARD_ASSETNAME);
            Source = new Rectangle(0,0, 200, Source.Height);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateWalking(currentKeyboardState);
            UpdateJump(currentKeyboardState);
            UpdateDuck(currentKeyboardState);

            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime, speed, direction);
        }

        private void UpdateWalking(KeyboardState currentKeyboardState)
        {
            if (currentState == State.Walking)
            {
                StopMoving();
                speed = new Vector2(WIZARD_SPEED, WIZARD_SPEED);
                UpdateDirectionsWithKeyboardState(currentKeyboardState, true, true);
            }
        }

        private void UpdateJump(KeyboardState currentKeyboardState)
        {
            if (currentState == State.Walking)
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
                    currentState = State.Walking;
                    StopMoving();
                }

                UpdateDirectionsWithKeyboardState(currentKeyboardState, true, false);
            }
        }

        private void UpdateDuck(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.D) == true)
            {
                if(currentState == State.Walking)
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
                    currentState = State.Walking;
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

    }
}
