using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TurnbaseGame.StateManagement;

namespace TurnbaseGame.Screens
{
    public class HomeScreen : GameScreen
    {
        private List<MenuEntry> _menuEntries = new List<MenuEntry>();
        private const string _menuTitle = "Turnbase Game";
        private int _selectedEntry;

        public HomeScreen()
        {
            var newGameMenu = new MenuEntry("New Game");
            var exitMenu = new MenuEntry("Exit");

            newGameMenu.Selected += NewGameMenuEntrySelected;
            exitMenu.Selected += ExitMenuEntrySelected;

            _menuEntries.Add(newGameMenu);
            _menuEntries.Add(exitMenu);
        }

        private void NewGameMenuEntrySelected(object sender, PlayerIndex playerIndex)
        {
            ScreenManager.AddScreen("New", new HomeScreen());
        }

        private void ExitMenuEntrySelected(object sender, PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            UpdateMenuEntryLocations();

            var graphics = ScreenManager.GraphicsDevice;
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.SpriteFont;

            spriteBatch.Begin();

            for (int i = 0; i < _menuEntries.Count; i++)
            {
                var menuEntry = _menuEntries[i];
                bool isSelected = i == _selectedEntry;
                menuEntry.Draw(this, gameTime, isSelected);
            }

            var titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            var titleOrigin = font.MeasureString(_menuTitle) / 2;
            var titleColor = new Color(192, 192, 192);
            const float titleScale = 1.25f;

            spriteBatch.DrawString(font, _menuTitle, titlePosition, titleColor, 0, titleOrigin, titleScale, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        private void UpdateMenuEntryLocations()
        {
            var position = new Vector2(0f, 175f);

            foreach (var menuEntry in _menuEntries)
            {
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;
                position.Y += menuEntry.GetHeight(this);

                menuEntry.Position = position;
            }
        }
    }
}
