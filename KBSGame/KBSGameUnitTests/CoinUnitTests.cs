using System;
using System.Windows;
using KBSGame;
using KBSGame.GameObjects;
using NUnit.Framework;

namespace KBSGameUnitTests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class CoinUnitTests
    {
       
        [SetUp]
        public void SetUp()
        {

            

            Obstacles.waardes.Clear();

        } 

        [Test]
        public void MoveRight_PlayerDontCollectACoin_IsFalse()
        {
            //Arrange
            Player player = new Player();
            bool eventRaised = false;

            //Act
            player.collectCoin += (sender, e) => { eventRaised = true; };

            player.MoveRight();

            //Assert
            Assert.IsFalse(eventRaised);
        }

        [Test]
        public void MoveRight_PlayerCollectACoin_IsTrue()
        {
            //Arrange
            Player player = new Player();
            bool eventRaised = false;
            Obstacles.waardes.Add("500c");

            //Act
            player.collectCoin += (sender, e) => { eventRaised = true; };

            player.MoveRight();

            //Assert
            Assert.IsTrue(eventRaised);
        }
        
    }
}
