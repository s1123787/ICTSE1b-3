using System;
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
    public class GameWonOverlayUnitTests
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
        public void GameVictory_PlayerCantMove_ReturnTrue()
        {
            Player.x = 150;
            Player.y = 150;
            game.GameVictory();
            game.Player.MoveRight();
            Assert.AreNotEqual(Player.x, 150);
        }

        [Test]
        public void GameVictory_BombExplodeStops_ReturnTrue()
        {
            Player.x = 150;
            Player.y = 150;
            game.bombx = 150;
            game.bomby = 0;
            game.GameVictory();
            game.bomby = 150;
            if (game.GameLost == true)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GameVictory_MovingObstacleCantMove_ReturnTrue()
        {
            bool test = true;
            
            MovingObstacle moving = new MovingObstacle(game, true);
            moving.SetX(100);
            moving.SetY(100);
            Obstacles.values.Add($"100100m");

            game.GameVictory();
            moving.MoveObstakelDown();
            
            if (Obstacles.values.Contains("100150m"))
            {
                test = false;
            }

            Assert.IsTrue(test);
        }

        [Test]
        public void GameVictory_CorrectFlagValues_ReturnTrue()
        {
            bool test = false;
            game.GameVictory();
            if(game.FreezePlayer == true && game.GameLost == false && game.GameWon == true && game.playing == false)
            {
                test = true;
            }
            Assert.IsTrue(test);
        }
    }
}
