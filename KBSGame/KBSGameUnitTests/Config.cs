using System;
using System.Windows;
using KBSGame;
using KBSGame.GameObjects;
using NUnit.Framework;

namespace KBSGameUnitTests
{
    [SetUpFixture]
    public class Config
    {
        Application app = new Application();

        [OneTimeSetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            Obstacles.waardes.Clear();

        }
    }
}
