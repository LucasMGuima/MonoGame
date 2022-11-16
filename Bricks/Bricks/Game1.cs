using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bricks
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        GameContent gameContent;

        private Paddle paddle;
        private Wall wall;
        private GameBorder gameBorder;
        private Ball ball;
        private Ball staticBall; // Usado para desenhar a imagem proximo ao contador de bolas restantes


        private int screenWidth = 0;
        private int screenHeight = 0;
        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;
        private bool readyToServeBall = true;
        private int ballsRemaining = 3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameContent = new GameContent(Content);
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //set game to 502x700 or screen max if smaller
            if (screenWidth >= 502)
            {
                screenWidth = 502;
            }
            if (screenHeight >= 700)
            {
                screenHeight = 700;
            }
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            //create game objects
            int paddleX = (screenWidth - gameContent.imgPaddle.Width) / 2;
            //we'll center the paddle on the screen to start
            int paddleY = screenHeight - 100;  //paddle will be 100 pixels from the bottom of the screen
            paddle = new Paddle(paddleX, paddleY, screenWidth, spriteBatch, gameContent);  // create the game paddle
            wall = new Wall(1, 50, spriteBatch, gameContent); //Create the wall
            gameBorder = new GameBorder(screenWidth, screenHeight, spriteBatch, gameContent);
            ball = new Ball(screenWidth, screenHeight, spriteBatch, gameContent);
            staticBall = new Ball(screenWidth, screenHeight, spriteBatch, gameContent);
            staticBall.X = 25;
            staticBall.Y = 25;
            staticBall.Visible = true;
            staticBall.UseRotation = false;
        }

        protected override void Update(GameTime gameTime)
        {
            if(IsActive == false) { return; }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            //processa o movimento do mause
            if(oldMouseState.X != newMouseState.X)
            {
                if(newMouseState.X >= 0 || newMouseState.X < screenWidth)
                {
                    paddle.MoveTo(newMouseState.X);
                }
            }

            //prcessa o click-esquerdo
            if(newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed && oldMouseState.X == newMouseState.X && oldMouseState.Y == newMouseState.Y)
            {
                ServeBall();
            }

            //processa os eventos do teclado
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                paddle.MoveLeft();
            }
            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                paddle.MoveRight();
            }
            if (oldKeyboardState.IsKeyUp(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Space) && readyToServeBall)
            {
                ServeBall();
            }

            //salva os estados antigos
            oldMouseState = newMouseState;
            oldKeyboardState = newKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            paddle.Draw();
            wall.Draw();
            gameBorder.Draw();
            if (ball.Visible)
            {
                bool inPlay = ball.Move(wall, paddle);
                if (inPlay)
                {
                    ball.Draw();
                }
                else
                {
                    ballsRemaining--;
                    readyToServeBall = true;
                }
            }
            staticBall.Draw();

            string scoreMsg = "Score: " + ball.Score.ToString("00000");
            Vector2 space = gameContent.labelFont.MeasureString(scoreMsg);
            spriteBatch.DrawString(gameContent.labelFont, scoreMsg, new Vector2((screenWidth - space.X) / 2, screenHeight - 40), Color.White);
            if (ball.bricksCleared >= 70)
            {
                ball.Visible = false;
                ball.bricksCleared = 0;
                wall = new Wall(1, 50, spriteBatch, gameContent);
                readyToServeBall = true;
            }
            if (readyToServeBall)
            {
                if (ballsRemaining > 0)
                {
                    string startMsg = "Press <Space> or Click Mouse to Start";
                    Vector2 startSpace = gameContent.labelFont.MeasureString(startMsg);
                    spriteBatch.DrawString(gameContent.labelFont, startMsg, new Vector2((screenWidth - startSpace.X) / 2, screenHeight / 2), Color.White);
                }
                else
                {
                    string endMsg = "Game Over";
                    Vector2 endSpace = gameContent.labelFont.MeasureString(endMsg);
                    spriteBatch.DrawString(gameContent.labelFont, endMsg, new Vector2((screenWidth - endSpace.X) / 2, screenHeight / 2), Color.White);
                }
            }
            spriteBatch.DrawString(gameContent.labelFont, ballsRemaining.ToString(), new Vector2(40, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //---------------MEUS METODOS----------------
        private void ServeBall()
        {
            if(ballsRemaining < 1)
            {
                ballsRemaining = 3;
                ball.Score = 0;
                wall = new Wall(1, 50, spriteBatch, gameContent);
            }
            readyToServeBall = false;
            float ballX = paddle.X + (paddle.Width)/2;
            float ballY = paddle.Y - ball.Height;
            ball.Launch(ballX, ballY, -3, -3);
        }
    }
}