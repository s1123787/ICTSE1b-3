using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using KBSGame;
using KBSGame.GameObjects;
using KBSGame.Model;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;


namespace KBSGameUnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class BombUnitTests
    {
        Game game;
        bool eventRaised;
        object eventSource;
        Application app = new Application();


        [SetUp]
        public void SetUp()
        {

            if (Application.ResourceAssembly == null)
                Application.ResourceAssembly = typeof(MainWindow).Assembly;

            game = new Game(new MainWindow(true), new Canvas(), 30, 10, 3, 5, 30, true);
            eventRaised = false;
        }

        //hier test je of speler over bom heen loopt en als dat niet zo is dat er dan niks gebeurd
        [Test]
        public void OnPlayerWalkedOverBomb_PlayerDontWalkedOverBomb_ReturnFalse()
        {
            eventRaised = false;
            // Act

            //Dit event wordt aangeroepen zodra speler over bom heen loopt
            game.Player.walkedOverBomb += (sender, e) => { eventRaised = true; };
            //speler beweegt 1 plek opzij
            game.Player.MoveLeft();
            Assert.IsFalse(eventRaised);
        }

        
        //hier kijk je of er een event wordt aangeroepen als speler of bom heen loopt
        [Test]
        public void OnPlayerWalkedOverBomb_PlayerWalkedOverBomb_ReturnTrue()
        {
            // Act
            bool test = false;
            //hier voeg je een bom toe op positie x=100 en y = 0
            Obstacles.values.Add("1000b");
            game.Player.walkedOverBomb += (sender, e) => { test = true; };
            //hier beweeg je de speler 4 keer zodat het op de bom komt, waardoor de event wordt aangeroepen
            game.Player.MoveRight();
            game.Player.MoveRight();
            game.Player.MoveRight();
            game.Player.MoveRight();
            Assert.IsTrue(test);
        }

        //hier test je of speler dood gaat als bom is aangeroepen en speler is in radius
        [Test]
        public void TimerTick_PlayerWalkedOverBombAndDontMove_ReturnFalse()
        {
            //dit is de positie waar de speler zich bevind
            Player.x = 100;
            Player.y = 100;
            //dit is de positie waar de bom zich bevind
            game.bombx = 100;
            game.bomby = 100;
            //het event wordt aangeroepen
            game.Timer_Tick(this, EventArgs.Empty);
            Assert.IsFalse(game.playing);
        }

        //hier test je of speler dood gaat als bom is aangeroepen en speler is in radius
        [Test]
        public void TimerTick_PlayerWalkedOverBombAndMoveOnePlace_ReturnFalse()
        {           
            game.bombx = 100;
            game.bomby = 100;
            //de speler is 1 plek opgeschoven van de plek waar de bom ontploft
            Player.x = 150;
            Player.y = 100;
            game.Timer_Tick(this, EventArgs.Empty);
            Assert.IsFalse(game.playing);
        }

        //hier test je of speler niet dood gaat als het ver genoeg is van ontploffing bom
        [Test]
        public void TimerTick_PlayerWalkedOverBombAndMovedTwoPlaces_ReturnTrue()
        {
            game.bombx = 100;
            game.bomby = 100;
            //de speler is 2 plekker opgeschoven waar bom is ontploft
            Player.x = 200;
            Player.y = 100;
            game.Timer_Tick(this, EventArgs.Empty);
            Assert.IsTrue(game.playing);
        }

        //hier test je of speler dood gaat als bom is aangeroepen en speler is in radius

        [Test]
        public void TimerTick2_PlayerWalkedOverBombAndDontMove_ReturnFalse()
        {
            //deze moet je aanroepen om foutmeldingen te voorkomen
            game.OnPlayerWalkedOverBomb(game, new GameEventArgs(100, 0, 0, 0));
            game.bombx = 100;
            game.bomby = 100;
            Player.x = 100;
            Player.y = 100;            
            game.Timer2_Tick(game, EventArgs.Empty);
            Assert.IsFalse(game.playing);
        }

        //hier test je of speler dood gaat als bom is aangeroepen en speler is in radius
        [Test]
        public void TimerTick2_PlayerWalkedOverBombAndMovedOnePlace_ReturnFalse()
        {
            game.OnPlayerWalkedOverBomb(game, new GameEventArgs(100, 0, 0, 0));
            game.bombx = 100;
            game.bomby = 100;
            Player.x = 150;
            Player.y = 100;
            game.Timer2_Tick(game, EventArgs.Empty);
            Assert.IsFalse(game.playing);
        }

        //hier test je of speler ver genoeg weg is van de ontploffing van bom
        [Test]
        public void TimerTick2_PlayerWalkedOverBombAndMovedTwoPlaces_ReturnTrue()
        {
            game.OnPlayerWalkedOverBomb(game, new GameEventArgs(100, 0, 0, 0));
            game.bombx = 100;
            game.bomby = 100;
            Player.x = 200;
            Player.y = 100;
            game.Timer2_Tick(game, EventArgs.Empty);
            Assert.IsTrue(game.playing);
        }

        //hier test je of bom wordt verwijderd uit lijst zodat het niet nog eens geactiveerd kan worden
        [Test]
        public void OnPlayerWalkedOverBomb_PlayerWalkedOverBombAndBombIsGone_ReturnTrue()
        {
            //je voegt bom toe op positie x=100 y=0
            Obstacles.values.Add("1000b");
            //speler loopt over bom waar de waardes van bom in worden meegegeven
            game.OnPlayerWalkedOverBomb(game, new GameEventArgs(100, 0, 0, 0));
            //hier kijk je of de bom nog in de lijst staat
            bool test = Obstacles.values.Contains("1000b");
            Assert.IsFalse(test);
        }
    }
}
