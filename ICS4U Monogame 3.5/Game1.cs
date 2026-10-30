using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ICS4U_Monogame_3._5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D carTexture;
        Rectangle carRect;

        Texture2D rocketTexture;
        Rectangle rocketRect;

        Texture2D asteroidTexture;
        Rectangle asteroidRect;

        bool isDraggingAsteroid;
        bool isDraggingCar;
        bool isDraggingRocket;

        MouseState currentMouseState;
        MouseState prevMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            asteroidRect = new Rectangle(10, 10, 50, 50);
            isDraggingAsteroid = false;
            carRect = new Rectangle(200, 200, 75, 25);
            isDraggingCar = false;
            rocketRect = new Rectangle(400, 100, 40, 75);
            isDraggingRocket = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            asteroidTexture = Content.Load<Texture2D>("images/asteroid");
            carTexture = Content.Load<Texture2D>("images/fast_car");
            rocketTexture = Content.Load<Texture2D>("images/rocket");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (NewClick() && asteroidRect.Contains(currentMouseState.Position))
                isDraggingAsteroid = true;
            else if (isDraggingAsteroid && currentMouseState.LeftButton == ButtonState.Released)
                isDraggingAsteroid = false;
            else if (isDraggingAsteroid)
                asteroidRect.Offset(currentMouseState.X - prevMouseState.X,
                currentMouseState.Y - prevMouseState.Y);

            if (NewClick() && carRect.Contains(currentMouseState.Position))
                isDraggingCar = true;
            else if (isDraggingCar && currentMouseState.LeftButton == ButtonState.Released)
                isDraggingCar = false;
            else if (isDraggingCar)
                carRect.Offset(currentMouseState.X - prevMouseState.X, 0);

            if (NewClick() && rocketRect.Contains(currentMouseState.Position))
                isDraggingRocket = true;
            else if (isDraggingRocket && currentMouseState.LeftButton == ButtonState.Released)
                isDraggingRocket = false;
            else if (isDraggingRocket)
                rocketRect.Offset(0, currentMouseState.Y - prevMouseState.Y);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(asteroidTexture, asteroidRect, Color.White);
            _spriteBatch.Draw(carTexture, carRect, Color.White);
            _spriteBatch.Draw(rocketTexture, rocketRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool NewClick()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;

        }
    }
}
