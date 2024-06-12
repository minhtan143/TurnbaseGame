using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace TurnbaseGame.StateManagement
{
    public class ScreenManager : DrawableGameComponent
    {
        private readonly Dictionary<string, GameScreen> _screens = new Dictionary<string, GameScreen>();
        private readonly InputState _input = new InputState();

        private GameScreen _currentSreen;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        public SpriteBatch SpriteBatch => _spriteBatch;
        public SpriteFont SpriteFont => _spriteFont;

        public ScreenManager(Game game) : base(game)
        {
            TouchPanel.EnabledGestures = GestureType.None;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            var content = Game.Content;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = content.Load<SpriteFont>("defaultFont");
        }

        public void AddScreen(string screenName, GameScreen screen)
        {
            screen.ScreenManager = this;
            _screens.Add(screenName, screen);
        }

        public void RemoveScreen(string name)
        {
            if (_screens.ContainsKey(name))
            {
                _screens[name].Exit();
                _screens.Remove(name);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _input.Update();

            if (_currentSreen != null)
                _currentSreen.HandleInput(gameTime, _input);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens.Values)
            {
                if (screen.Visible)
                    screen.Draw(gameTime);
            }
        }
    }
}
