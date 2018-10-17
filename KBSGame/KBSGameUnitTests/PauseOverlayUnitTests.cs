using System;
using System.Windows;
using System.Windows.Controls;
using KBSGame;
using KBSGame.Model;
using NUnit.Framework;
using System.Threading;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]    
    public class PauseOverlayUnitTests
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
        public void playerCantMoveOnPause()
        {
            Player.x = 150;
            Player.y = 150;
            game.OnEsqKeyIsPressed(this, EventArgs.Empty);
            game.Player.MoveRight();
            Assert.AreNotEqual(Player.x, 150);
        }
        
        [Test]
        public void BombExplodeStopsOnPause()
        {
            Player.x = 150;
            Player.y = 150;
            game.testx = 150;
            game.testy = 0;
            game.OnEsqKeyIsPressed(this, EventArgs.Empty);
            game.testy = 150;
            if(game.GameLost == true)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void EnterDoesNothingOnGameOverOverlay()
        {
            game.playing = true;
            game.GameOver();
            game.OnEnterKeyIsPressed(this, EventArgs.Empty);
            Assert.False(game.playing);
        }
        [Test]
        public void EnterDoesNothingOnGameWonOverlay()
        {
            game.playing = true;
            game.GameVictory();
            game.OnEnterKeyIsPressed(this, EventArgs.Empty);
            Assert.False(game.playing);
        }
        [Test]
        public void CantOpenPauseWhenOtherMenuOpen()
        {
            game.playing = true;
            game.GameOver();
            game.OnEsqKeyIsPressed(this, EventArgs.Empty);
            Assert.False(game.pauseActivated);
        }
        [Test]
        public void EnterThenEscapeOnGameOverDoesntTriggerPauseOverlay()
        {
            game.playing = true;
            game.GameOver();
            game.OnEnterKeyIsPressed(this, EventArgs.Empty);
            game.OnEsqKeyIsPressed(this, EventArgs.Empty);
            Assert.False(game.pauseActivated);
        }
    }
}
