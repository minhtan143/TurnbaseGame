using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TurnbaseGame.Screens;
using TurnbaseGame.StateManagement;

namespace TurnbaseGame
{
    public class TurnbaseGame : Game
    {
        private GraphicsDeviceManager _graphics;

        public TurnbaseGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            screenManager.AddScreen("Home", new HomeScreen());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
