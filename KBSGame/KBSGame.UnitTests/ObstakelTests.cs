using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KBSGame.UnitTests
{
    [TestClass]
    public class ObstakelTests
    {
        [TestMethod]
        public void AssignPosition_PositionIsAvailableAndNotAtBeginOrEnd_XAndYWereSet()
        {
            // Arrange
            var Obstakel = new Obstakel("Bom");
            List<string> waardes = new List<string>();
            waardes.Add("400200");

            // Act


            // Assert
            Assert.AreNotEqual(Obstakel.x, 0);
            Assert.AreNotEqual(Obstakel.x, 50);
            Assert.AreNotEqual(Obstakel.x, 750);
            Assert.AreNotEqual(Obstakel.x, 700);
            Assert.AreNotEqual(Obstakel.y, 0);
            Assert.AreNotEqual(Obstakel.y, 50);
            
            
        }
    }
}
