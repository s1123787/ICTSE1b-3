using System;
using System.Windows;
using System.Windows.Controls;
using KBSGame;
using KBSGame.GameObjects;
using NUnit.Framework;
using System.Threading;
using KBSGame.Model;
using System.Diagnostics;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class MovingObstakelUnitTest
    {
        Game game;
        bool eventRaised;
        Application app = new Application();
        private TestContext testContextInstance;

        [SetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            game = new Game(new MainWindow(true), new Canvas(), 30, 10, 0, 5, 30, true);
            

        }

        //Check if moving obstakel moves right and hits next grid.
        [Test]
        public void PlayerMove_PlayerHitsMovingObstakel_ReturnTrue()
        {
            // Act
            bool test = false;

            //Move the player 4 times to hit obstacle
            game.Player.MoveRight();
            game.Player.MoveRight();
            game.Player.MoveRight();
            game.Player.MoveRight();

            //add obstacle on position x=100 en y = 0
            MovingObstacle moving = new MovingObstacle(game);
            moving.SetX(100);
            moving.SetY(0);

            //check if exists
            if(Obstakels.waardes.Contains($"1000m"))
            {
                //check if player hit Obstakel
                test = true;
            }
            Assert.IsTrue(test);
        }

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        
        [Test]
        public void MoveObstacleRight_MovingObstakelMovesRight_ReturnTrue()
        {
            bool test = false;

            //add obstacle on position x=100 en y = 0
            MovingObstacle moving = new MovingObstacle(game, true);
            moving.SetX(100);
            moving.SetY(0);
            Obstakels.waardes.Add($"1000m");

            moving.MoveObstakelRight();
            
            
            //check if Moving Obstakels moved 50px to the right
            if (Obstakels.waardes.Contains("1500m"))
            {
                test = true;
            }

            Assert.IsTrue(test);
        }

        
        [Test]
        public void MoveObstacleLeft_MovingObstakelMovesLeft_ReturnTrue()
        {
            bool test = false;

            //add obstacle on position x=100 en y = 0
            MovingObstacle moving = new MovingObstacle(game, true);
            moving.SetX(100);
            moving.SetY(0);
            Obstakels.waardes.Add($"1000m");

            moving.MoveObstakelLeft();

            //check if Moving Obstakels moved 50px to the right
            if (Obstakels.waardes.Contains("500m"))
            {
                test = true;
            }

            Assert.IsTrue(test);
        }


        [Test]
        public void MoveObstacleUp_MovingObstakelMovesUp_ReturnTrue()
        {
            bool test = false;

            //add obstacle on position x=100 en y = 0
            MovingObstacle moving = new MovingObstacle(game, true);
            moving.SetX(100);
            moving.SetY(100);
            Obstakels.waardes.Add($"100100m");

            moving.MoveObstakelUp();

            //check if Moving Obstakels moved 50px to the right
            if (Obstakels.waardes.Contains("10050m"))
            {
                test = true;
            }

            Assert.IsTrue(test);
        }

        [Test]
        public void MoveObstacleDown_MovingObstakelMovesDown_ReturnTrue()
        {
            bool test = false;

            //add obstacle on position x=100 en y = 0
            MovingObstacle moving = new MovingObstacle(game, true);
            moving.SetX(100);
            moving.SetY(100);
            Obstakels.waardes.Add($"100100m");
            

            moving.MoveObstakelDown();

            //check if Moving Obstakels moved 50px to the right
            if (Obstakels.waardes.Contains("100150m"))
            {
                test = true;
            }

            Assert.IsTrue(test);
        }
    }
}
