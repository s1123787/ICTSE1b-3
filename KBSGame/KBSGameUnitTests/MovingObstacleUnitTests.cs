using System;
using System.Windows;
using KBSGame;
using KBSGame.GameObjects;
using NUnit.Framework;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class MovingObstacleUnitTests
    {

        [SetUp]
        public void SetUp()
        {           
            Obstacles.waardes.Clear();
        } 

        [Test]
        public void MoveRight_PlayerDontWalkedOnMovingObstacle_ReturnFalse()
        {
            //Arrange
            Player player = new Player();
            bool eventRaised = false;

            //Act
            player.walkedOnMovingObstacle += (sender, e) => { eventRaised = true; };

            player.MoveRight();

            //Assert
            Assert.IsFalse(eventRaised);

        }   
        
        [Test]
        public void MoveRight_PlayerWalkedOnMovingObstacle_ReturnTrue()
        {
            //Arrange
            Player player = new Player();
            bool eventRaised = false;
            Obstacles.waardes.Add("500m");

            //Act
            player.walkedOnMovingObstacle += (sender, e) => { eventRaised = true; };

            player.MoveRight();

            //Assert
            Assert.IsTrue(eventRaised);
        }
    }
}
