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
    public class PlayerUnitTests
    {
        

        [SetUp]
        public void SetUp()
        {            
            Player.x = 5;
            Player.y = 5;
            Obstacles.waardes.Clear();
        }

        [Test]
        public void Eplayer_PlayerSpawnsAtStartPoint_ReturnEqual()
        {
            //Arrange
            Player player = new Player();
            
            //Act
            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);

        }

        [Test]
        public void MoveRight_PlayerMovesRightFromStart_ReturnTrue()
        {
            //Arrange
            Player player = new Player();
            
            //Act
            player.MoveRight();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(55, x);
            Assert.AreEqual(5, y);
        }

        [Test]
        public void MoveLeft_PlayerMovesLeftFromCertainPosition_ReturnTrue()
        {
            //Arrange
            Player player = new Player();

            player.MoveRight(); //to make sure player is on position to go left

            //Act
            player.MoveLeft();
            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);
        }

        [Test]
        public void MoveDown_PlayerMovesDownFromStart_ReturnTrue()
        {
            //Arrange
            Player player = new Player();
            
            //Act
            player.MoveDown();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(55, y);

        }

        [Test]
        public void MoveUp_PlayerMovesUpFromCertainPosition_ReturnTrue()
        {
            //Arrange
            Player player = new Player();

            player.MoveDown(); //to make sure player can move up

            //Act
            player.MoveUp();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);
            
        }
        [Test]
        public void MoveRight_PlayerMovesRightToBorder_ReturnTrue()
        {
            //Arrange
            Player player = new Player();           

            //Act
            for(int i = 0; i <= 40; i++) //player tried to move right 40 times
            {
                player.MoveRight();
            }

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(755, x); //check if player can't go further dan 755
            Assert.AreEqual(5, y);

        }

        [Test]
        public void MoveLeft_PlayerMovesLeftToBorder_ReturnTrue()
        {
            //Arrange
            Player player = new Player();

            //Act
            player.MoveLeft();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);
        }

        [Test] 
        public void MoveDown_PlayerMovesDownToBorder_ReturnTrue()
        {
            //Arrange
            Player player = new Player();

            //Act
            for (int i = 0; i < 40; i++) //let player walk 40 times down to make sure it get's by border
            {
                player.MoveDown();
            }

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(555, y);
        }

        [Test]
        public void MoveUp_PlayerMovesUpToBorder_ReturnTrue()
        {
            //Arrange
            Player player = new Player();

            //Act
            player.MoveUp();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);
        }

        [Test]
        public void MoveRight_PlayerMovesRightToTree_ReturnTrue()
        {
            //Arrange
            Player player = new Player();
            Obstacles.waardes.Add("500t");

            //Act
            player.MoveRight();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);
        } 
        
        [Test]
        public void MoveLeft_PlayerMovesLeftToTree_ReturnTrue()
        {
            //Arrange
            Player player = new Player();
            player.MoveRight(); //make sure player is on right of a tree
            player.MoveRight();
            Obstacles.waardes.Add("500t"); //add a tree to the game

            //Act assert
            player.MoveLeft();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(105, x);
            Assert.AreEqual(5, y);            
        }

        [Test]
        public void MoveDown_PlayerMovesDownToTree_ReturnTrue()
        {
            //Arrange
            Player player = new Player();
            Obstacles.waardes.Add("050t");

            //Act
            player.MoveDown();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(5, y);
        }

        [Test]
        public void MoveUp_PlayerMovesUpToTree_ReturnTrue()
        { 
            //Arrange
            Player player = new Player();
            player.MoveDown();
            player.MoveDown();
            Obstacles.waardes.Add("050t");

            //Act
            player.MoveUp();

            double x = Player.x;
            double y = Player.y;

            //Assert
            Assert.AreEqual(5, x);
            Assert.AreEqual(105, y);
        }        
    }
}
