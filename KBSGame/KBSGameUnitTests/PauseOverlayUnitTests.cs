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

            game = new Game(new MainWindow(true), new Canvas(), 30, 10, 3, 5, 30, true);

        }

        [Test]
        public void OnEscKeyIsPressed_CheckIfPlayerDidntMove_XNotEqual()
        {
            Player.x = 150;
            Player.y = 150;

            game.OnEscKeyIsPressed(this, EventArgs.Empty);
            game.Player.MoveRight();

            Assert.AreNotEqual(Player.x, 150);
        }
        
        [Test]
        public void OnEscKeyIsPressed_BombDoesntExplodeOnPause_IfGameLostFail()
        {
            Player.x = 150;
            Player.y = 150;
            game.bombx = 150;
            game.bomby = 0;

            game.OnEscKeyIsPressed(this, EventArgs.Empty);
            game.bomby = 150;

            if(Game.GameLost == true)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void OnEnterKeyIsPressed_EnterDoesntDoAnythingOnGameOverOverlay_ReturnFalse()
        {
            Game.playing = true;
            game.GameOver();
            game.OnEnterKeyIsPressed(this, EventArgs.Empty);

            Assert.False(Game.playing);
        }
        [Test]
        public void OnEnterKeyIsPressed_EnterDoesntDoAnythingOnGameWonOverlay_ReturnFalse()
        {
            Game.playing = true;
            game.GameVictory();
            game.OnEnterKeyIsPressed(this, EventArgs.Empty);

            Assert.False(Game.playing);
        }
        [Test]
        public void OnEscKeyIsPressed_PauseMenuDoesntTriggerWhenOtherMenuIsActive_ReturnFalse()
        {
            Game.playing = true;
            game.GameOver();
            game.OnEscKeyIsPressed(this, EventArgs.Empty);

            Assert.False(game.pauseActivated);
        }
        [Test]
        public void OnEscKeyIsPressed_EnterThenEscapeOnGameOverDoesntTriggerPauseOverlay_ReturnFalse()
        {
            Game.playing = true;
            game.GameOver();
            game.OnEnterKeyIsPressed(this, EventArgs.Empty);
            game.OnEscKeyIsPressed(this, EventArgs.Empty);

            Assert.False(game.pauseActivated);
        }
    }
}
