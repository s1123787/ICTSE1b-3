using System;
using System.Windows;
using System.Windows.Controls;
using KBSGame;
using KBSGame.GameObjects;
using NUnit.Framework;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class MenuButtonUnitTests
    {
        Game game;
        Application app = new Application();

        [SetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            game = new Game(new MainWindow(), new Canvas(), 30, 10, 3, 5, 30);

        }

        [Test]
        public void Menu_ClickMainWindowExists_ReturnFalse()
        {
            game.pauseOverlay.menu.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            Assert.IsNull(game.mainWindow);
        }
    }
}
