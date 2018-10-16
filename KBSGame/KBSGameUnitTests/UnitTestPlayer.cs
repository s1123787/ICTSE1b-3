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

            g = new Game(new MainWindow(), new Canvas(), 30, 10, 3, 5, 30);
            player = g.Player.Eplayer;
        }

        [Test]
        public void PlayerSpawnsAtStartPoint()
        {
            double x = Canvas.GetLeft(g.Player.Eplayer);
            double y = Canvas.GetTop(g.Player.Eplayer);

            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);

        }

        [Test]
        public void PlayerMoves()
        {
            double x = Canvas.GetLeft(player);
            double y = Canvas.GetTop(player);

            g.Player.MoveRight();

            double x2 = Canvas.GetLeft(player);
            double y2 = Canvas.GetTop(player);

            Assert.AreEqual(x + 50, x2);

            g.Player.MoveDown();

            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            Assert.AreEqual(y2 + 50, y);

            g.Player.MoveLeft();

            x2 = Canvas.GetLeft(player);
            y2 = Canvas.GetTop(player);

            Assert.AreEqual(x - 50, x2);

            g.Player.MoveUp();

            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            Assert.AreEqual(y2 - 50, y);

        }

        [Test]
        public void MoveTowardsBorder()
        {
            g.Player.MoveLeft();
            double x = Canvas.GetLeft(player);
            Assert.AreEqual(5, x);

            g.Player.MoveUp();
            double y = Canvas.GetTop(player);
            Assert.AreEqual(5, y);
        }

        [Test]
        public void MoveTowardsTree()
        {
            Obstakels.waardes.Add("1000t");

            g.Player.MoveRight();
            Assert.AreEqual(55, Canvas.GetLeft(player));

            g.Player.MoveRight();
            Assert.AreEqual(55, Canvas.GetLeft(player));
        }

        [Test]
        public void PlayerCollectsCoin_CounterPlus1()
        {
            Obstakels.waardes.Add("0100c");

            g.Player.MoveDown();
            g.Player.MoveDown();
            g.Player.MoveDown();

            Assert.AreEqual(1, g.CollectedCoins);
        }

        [Test]
        public void PlayerCollectsCoin_CantCollectSameCoinTwice()
        {
            Obstakels.waardes.Add("0100c");

            g.Player.MoveDown();
            g.Player.MoveDown();
            g.Player.MoveDown();

            Assert.AreEqual(1, g.CollectedCoins);

            g.Player.MoveUp();
            g.Player.MoveUp();

            Assert.AreEqual(1, g.CollectedCoins);

        }

        [Test]
        public void EndPointAvailable()
        {
            Assert.IsFalse(g.EndPointIsShown);

            g.CollectedCoins = 4;
            Obstakels.waardes.Add("0100c");
            g.Player.MoveDown();
            g.Player.MoveDown();
            g.Player.MoveDown();

            Assert.IsTrue(g.EndPointIsShown);
        }
    }
}
