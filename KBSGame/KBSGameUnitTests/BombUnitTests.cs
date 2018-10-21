using System;
using System.Windows;
using KBSGame;
using KBSGame.GameObjects;
using KBSGame.Model;
using NUnit.Framework;

namespace KBSGameUnitTests
{
    

    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class BombUnitTests
    {
      
        [SetUp] 
        public void SetUp()
        {
            Obstacles.waardes.Clear();
        }
        //test if player don't walked on bomb
        [Test]
        public void OnPlayerWalkedOverBomb_PlayerDontWalkedOverBomb_ReturnFalse()
        {

            //arrange            
            Player player = new Player(); //initialize player
            bool eventRaised = false;            

            //act

            player.walkedOverBomb += (sender, e) => { eventRaised = true; }; //this will be triggered when event is raised
            player.MoveRight();
            player.MoveRight();

            //Assert

            Assert.IsFalse(eventRaised);

        }

        //test if player walked over bomb
        [Test]
        public void OnPlayerWalkedOverBomb_PlayerWalkedOverBomb_ReturnTrue()
        {
            //Arrange 
            Player player = new Player();
            bool eventRaised = false;
            Obstacles.waardes.Add("500b"); //add an bomb to the field

            //Act

            player.walkedOverBomb += (sender, e) => { eventRaised = true; };
            player.MoveRight();
            player.MoveRight();

            //Assert
            Assert.IsTrue(eventRaised);

        }
        


    } 
}
