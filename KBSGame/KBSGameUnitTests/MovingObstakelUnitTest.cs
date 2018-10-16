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

            game = new Game(new MainWindow(), new Canvas(), 30, 10, 0, 5, 30);
            

        }

        //Check if moving obstakel moves right and hits next grid.
        [Test]
        public void PlayerHitsMovingObstakel_ReturnTrue()
        {
            // Act
            bool test = false;

            //hier beweeg je de speler 4 keer zodat het op het obstakel komt, waardoor de event wordt aangeroepen
            game.Player.MoveRight();
            game.Player.MoveRight();
            game.Player.MoveRight();
            game.Player.MoveRight();

            //hier voeg je een moving obstakel toe op positie x=100 en y = 0
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
        public void MovingObstakelMovesRight_ReturnTrue()
        {
            bool test = false;

            //hier voeg je een moving obstakel toe op positie x=100 en y = 0
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
        public void MovingObstakelMovesLeft_ReturnTrue()
        {
            bool test = false;

            //hier voeg je een moving obstakel toe op positie x=100 en y = 0
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
        public void MovingObstakelMovesUp_ReturnTrue()
        {
            bool test = false;

            //hier voeg je een moving obstakel toe op positie x=100 en y = 0
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
        public void MovingObstakelMovesDown_ReturnTrue()
        {
            bool test = false;

            //hier voeg je een moving obstakel toe op positie x=100 en y = 0
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
