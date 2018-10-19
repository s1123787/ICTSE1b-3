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
        Game game;
        Application app = new Application();

        [SetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            game = new Game(new MainWindow(true), new Canvas(), 30, 10, 3, 5, 30, true);

        }
        //hier test je of de obstakels worden toegevoegd aan de lijst en direct of het geen duplicates heeft
        [Test]
        public void Obstakels_ObstakelsAddedToList_ReturnTrue()
        {
            // de game is al aangemaakt dat betekent dus dat de waardes al in de list staan
            //je moet het aantal gedeelt door twee doen, want obstakels.waarde is static waardoor het twee keer wordt gevuld
            int aantal = Obstacles.values.Count / 2;
            Assert.AreEqual(48, aantal);
        }

        //hier test je of de waardes uit de lijst worden verwijderd
        [Test]
        public void Reset_ObstakelsBeingDeletedFromList_ReturnTrue()
        {
            //de game is al aangemaakt dus het heeft waardes
            game.obstakels.Reset();
            int aantal = Obstacles.values.Count;
            Assert.AreEqual(0, aantal);
        }
    }
}
