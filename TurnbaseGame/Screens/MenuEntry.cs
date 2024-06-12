using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurnbaseGame.StateManagement;

namespace TurnbaseGame.Screens
{
    public class MenuEntry
    {
        private string _text;
        private float _selectionFade;
        private Vector2 _position;

        public string Text
        {
            private get => _text;
            set => _text = value;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public event EventHandler<PlayerIndex> Selected;
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            Selected?.Invoke(this, playerIndex);
        }

        public MenuEntry(string text)
        {
            _text = text;
        }

        public virtual int GetHeight(GameScreen screen)
        {
            return screen.ScreenManager.SpriteFont.LineSpacing;
        }

        public virtual int GetWidth(GameScreen screen)
        {
            return (int)screen.ScreenManager.SpriteFont.MeasureString(Text).X;
        }

        public virtual void Update(GameTime gameTime, bool isSelected)
        {
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                _selectionFade = Math.Min(_selectionFade + fadeSpeed, 1);
            else
                _selectionFade = Math.Max(_selectionFade - fadeSpeed, 0);
        }

        public virtual void Draw(GameScreen gameScreen, GameTime gameTime, bool isSelected)
        {
            var color = isSelected ? Color.Yellow : Color.White;

            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + 1;
            float scale = 1 + pulsate * 0.05f * _selectionFade;

            var spriteBatch = gameScreen.ScreenManager.SpriteBatch;
            var font = gameScreen.ScreenManager.SpriteFont;

            var origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, _text, _position, color, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
