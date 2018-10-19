using System.Windows.Controls;
using System.Windows;
using NUnit.Framework;
using System.Threading;
using System.Windows.Shapes;
using KBSGame;
using KBSGame.GameObjects;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class UnitTestPlayer
    {
        Game g;
        Application app = new Application();
        Ellipse player;

        [SetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            g = new Game(new MainWindow(true), new Canvas(), 30, 10, 3, 5, 30, true);
            player = g.Player.Eplayer;
        }

        [Test]
        public void Eplayer_PlayerSpawnsAtStartPoint_ReturnEqual()
        {
            //Arrange
            double x = Canvas.GetLeft(g.Player.Eplayer);
            double y = Canvas.GetTop(g.Player.Eplayer);
            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);

        }

        [Test]
        public void Move_PlayerMovesInEveryDirection_ReturnEqual()
        {
            //Arrange
            double x = Canvas.GetLeft(player);
            double y = Canvas.GetTop(player);
            
            //Act
            g.Player.MoveRight();

            double x2 = Canvas.GetLeft(player);
            double y2 = Canvas.GetTop(player);
            
            //Assert
            Assert.AreEqual(x + 50, x2);
            
            //Act
            g.Player.MoveDown();

            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            Assert.AreEqual(y2 + 50, y);

            //Act
            g.Player.MoveLeft();

            x2 = Canvas.GetLeft(player);
            y2 = Canvas.GetTop(player);
            
            //Assert
            Assert.AreEqual(x - 50, x2);

            //Act
            g.Player.MoveUp();

            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            //Assert
            Assert.AreEqual(y2 - 50, y);

        }

        [Test]
        public void Move_MoveTowardsBorder_ReturnEqual()
        {
            //Act
            g.Player.MoveLeft();
            
            //Assert
            double x = Canvas.GetLeft(player);
            Assert.AreEqual(5, x);
            
            //Act
            g.Player.MoveUp();

            //Assert
            double y = Canvas.GetTop(player);
            Assert.AreEqual(5, y);
        }

        [Test]
        public void Move_MoveTowardsTree_ReturnEqual()
        {
            //Arrange
            Obstacles.values.Add("1000t");

            //Act
            g.Player.MoveRight();

            //Assert
            Assert.AreEqual(55, Canvas.GetLeft(player));

            //Act
            g.Player.MoveRight();

            //Assert
            Assert.AreEqual(55, Canvas.GetLeft(player));
        }

        [Test]
        public void Move_PlayerCollectsCoinAndCounterPlus1_ReturnEqual()
        {
            Obstacles.values.Add("0100c");

            g.Player.MoveDown();
            g.Player.MoveDown();
            g.Player.MoveDown();

            Assert.AreEqual(1, g.CollectedCoins);
        }

        [Test]
        public void Move_PlayerCollectsCoinAndCantCollectSameCoinTwice_ReturnEqual()
        {
            //Arrange
            Obstacles.values.Add("0100c");

            //Act
            g.Player.MoveDown();
            g.Player.MoveDown();
            g.Player.MoveDown();

            //Assert
            Assert.AreEqual(1, g.CollectedCoins);

            //Act
            g.Player.MoveUp();
            g.Player.MoveUp();

            //Assert
            Assert.AreEqual(1, g.CollectedCoins);

        }

        [Test]
        public void Move_EndPointIsAvailableWhenPickUpFifthCoin_ReturnTrue()
        {
            //Assert
            Assert.IsFalse(g.EndPointIsShown);

            //Arrange
            g.CollectedCoins = 4;
            Obstacles.values.Add("0100c");

            //Act
            g.Player.MoveDown();
            g.Player.MoveDown();
            g.Player.MoveDown();

            //Assert
            Assert.IsTrue(g.EndPointIsShown);
        }
    }
}
