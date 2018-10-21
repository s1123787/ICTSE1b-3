using System;
using System.Windows;
using System.Windows.Controls;
using KBSGame;
using KBSGame.GameObjects;
using NUnit.Framework;
using System.Threading;

namespace KBSGameUnitTests
{
    
    [TestFixture, Apartment(ApartmentState.STA)]
    public class ObstakelsUnitTests
    {
        [SetUp]
        public void SetUp()
        {
           Obstacles.waardes.Clear();
        }
           
        //hier test je of de obstakels worden toegevoegd aan de lijst en direct of het geen duplicates heeft
        [Test]
        public void Obstakels_ObstakelsAddedToList_ReturnTrue()
        {
            //Arrange
            Obstacles obstacles = new Obstacles(30, 10, 3, 5, true);
            int aantal = Obstacles.waardes.Count;

            //Act

            //Assert
            Assert.AreEqual(48, aantal);
        }
        
    }
}
