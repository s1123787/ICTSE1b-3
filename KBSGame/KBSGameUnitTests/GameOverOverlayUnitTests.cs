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
            //Arrange
            Player.x = 150;
            Player.y = 150;
            //Act
            game.GameOver();
            game.Player.MoveRight();
            //Assert
            Assert.AreEqual(Player.x, 150);
        }

        [Test]
        public void GameOver_BombExplodeStops_ReturnTrue()
        {
            //Arrange
            Player.x = 150;
            Player.y = 150;
            game.bombx = 150;
            game.bomby = 0;
            //Act
            game.GameOver();
            game.bomby = 150;
            //Assert
            Assert.IsFalse(game.explosionIsGoingToTakePlace);
        }

        [Test]
        public void GameOver_MovingObstacleCantMove_ReturnTrue()
        {
            //Arrange
            bool test = true;
            MovingObstacle moving = new MovingObstacle(true, 100, 100);
            moving.SetX(100);
            moving.SetY(100);
            Obstacles.waardes.Add($"100100m");
            //Act
            game.GameOver();
            moving.MoveObstakelDown();
            //Assert
            if (Obstacles.waardes.Contains("100150m"))
            {
                test = false;
            }
            Assert.IsTrue(test);
        }

        [Test]
        public void GameOver_CorrectFlagValues_ReturnTrue()
        {
            //Arrange
            bool test = false;
            //Act
            game.GameOver();
            //Assert
            if (game.FreezePlayer == true && Game.GameLost == true && Game.GameWon == false && Game.playing == false)
            {
                test = true;
            }
            Assert.IsTrue(test);
        }
    }
}
