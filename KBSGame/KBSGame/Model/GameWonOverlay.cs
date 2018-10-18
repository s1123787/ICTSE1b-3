using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KBSGame.View;

namespace KBSGame.Model
{
    public class GameWonOverlay : Overlay
    {
        public Image VictorySprite;
        public Button again;
        public int VictorySpriteX, VictorySpriteY, againX, againY, menuX, menuY;

        public GameWonOverlay(Game g) : base(g)
        {
            //Create new image for sprite
            VictorySprite = new Image
            {
                Width = 400,
                Height = 200,
                Name = "VictorySprite"
            };
            //Set source of VictorySprite
            VictorySprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/victory-sprite.png"));

            //Create new button template for restart button
            ControlTemplate againButtonTemplate = new ControlTemplate(typeof(Button));
            var againButtonImage = new FrameworkElementFactory(typeof(Image));
            againButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/play-again-button.png", UriKind.RelativeOrAbsolute)));
            againButtonTemplate.VisualTree = againButtonImage;
            //Create new button to restart the game
            again = new Button
            {
                Width = 125,
                Height = 45,
                Name = "againButton",
                Template = againButtonTemplate
            };
            //Subscribe the again button to the method it needs to run
            again.Click += Again_Click;

            //Set coardinate values
            VictorySpriteX = 201;
            VictorySpriteY = 140;
            againX = 251;
            againY = 300;
            menuX = 424;
            menuY = 300;
        }

        //Actions to perform when play again button is clicked
        private void Again_Click(object sender, RoutedEventArgs e)
        {
            //Method to reset the game
            game.PlayAgain();
            //Clean up victory overlay
            game.RemoveGameWonOverlay(this, game.GameCanvas);
        }
    }
}
