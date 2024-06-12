using Microsoft.Xna.Framework;

namespace TurnbaseGame.StateManagement
{
    public abstract class GameScreen
    {
        private ScreenManager _screenManager;
        public bool Visible { get; set; }

        public ScreenManager ScreenManager
        {
            get => _screenManager;
            internal set => _screenManager = value;
        }

        protected GameScreen() 
        {
            Visible = true;
        }

        public virtual void Active() { }
        public virtual void HandleInput(GameTime gameTime, InputState inputState) { }
        public virtual void Draw(GameTime gameTime) { }

        public void Exit()
        {
            Visible = false;
        }
    }
}
