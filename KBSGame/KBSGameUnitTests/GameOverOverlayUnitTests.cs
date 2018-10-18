using System.Windows;
using System.Windows.Controls;
using KBSGame;
using KBSGame.Model;
using NUnit.Framework;
using System.Threading;
using KBSGame.GameObjects;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class GameOverOverlayUnitTests
    {
        Game game;
        Application app = new Application();

        [SetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            game = new Game(new MainWindow(true), new Canvas(), 0, 0, 0, 5, 30, true);

        }

        [Test]
        public void GameOver_PlayerCantMove_ReturnTrue()
        {
            Player.x = 150;
            Player.y = 150;
            game.GameOver();
            game.Player.MoveRight();
            Assert.AreNotEqual(Player.x, 150);
        }

        [Test]
        public void GameOver_BombExplodeStops_ReturnTrue()
        {
            Player.x = 150;
            Player.y = 150;
            game.bombx = 150;
            game.bomby = 0;
            game.GameOver();
            game.bomby = 150;
            if (game.GameLost == true)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GameOver_MovingObstacleCantMove_ReturnTrue()
        {
            bool test = true;

            MovingObstacle moving = new MovingObstacle(game, true);
            moving.SetX(100);
            moving.SetY(100);
            Obstakels.waardes.Add($"100100m");

            game.GameOver();
            moving.MoveObstakelDown();

            if (Obstakels.waardes.Contains("100150m"))
            {
                test = false;
            }

            Assert.IsTrue(test);
        }

        [Test]
        public void GameOver_CorrectFlagValues_ReturnTrue()
        {
            bool test = false;
            game.GameOver();
            if (game.FreezePlayer == true && game.GameLost == true && game.GameWon == false && game.playing == false)
            {
                test = true;
            }
            Assert.IsTrue(test);
        }
    }
}
