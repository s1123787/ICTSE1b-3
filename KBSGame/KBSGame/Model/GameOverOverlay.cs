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
    public class GameOverOverlay : Overlay
    {
        public Image GameOverSprite;
        public Button again;
        public int GameOverSpriteX, GameOverSpriteY, againX, againY, menuX, menuY;
        public delegate void AgainIsPressed(object source, EventArgs e);
        public event AgainIsPressed againIsPressed;

        public GameOverOverlay(Game g)
        {
            //Create new image for sprite
            GameOverSprite = new Image
            {
                Width = 400,
                Height = 200,
                Name = "GameOverSprite"
            };
            //Set source of GameOverSprite
            GameOverSprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/game-over-sprite.png"));

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
            GameOverSpriteX = 201;
            GameOverSpriteY = 140;
            againX = 251;
            againY = 300;
            menuX = 424;
            menuY = 300;
        }

        //Actions to perform when play again button is clicked
        private void Again_Click(object sender, RoutedEventArgs e)
        {
            OnAgainIsPressed();
            //Method to reset the game
            //game.PlayAgain();
            //Clean up game over overlay
            //game.RemoveGameOverOverlay(this, game.GameCanvas);
        }

        protected virtual void OnAgainIsPressed()
        {
            againIsPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
