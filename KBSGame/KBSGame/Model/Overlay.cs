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

namespace KBSGame.Model
{
    public class Overlay
    {
        protected MainWindow MainWindow;
        protected Canvas GameCanvas;
        protected Rectangle background;
        protected Button menu;
        protected Game game;

        public Overlay(MainWindow mw, Canvas canvas, Game g)
        {
            game = g;
            MainWindow = mw;
            GameCanvas = canvas;
            background = new Rectangle
            {
                Width = 400,
                Height = 250,
                Fill = Brushes.DimGray,
                Opacity = 0.90
            };

            //Create new button template for menu button
            ControlTemplate menuButtonTemplate = new ControlTemplate(typeof(Button));
            var menuButtonImage = new FrameworkElementFactory(typeof(Image));
            menuButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/menu-button.png", UriKind.RelativeOrAbsolute)));
            menuButtonTemplate.VisualTree = menuButtonImage;

            //Create new button to go to the game menu
            menu = new Button
            {
                Width = 125,
                Height = 45,
                Name = "menuButton",
                Template = menuButtonTemplate
            };

            //Subscribe the button to the method it needs to run
            menu.Click += Menu_Click;
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //Closes the application
            Application.Current.Shutdown();
        }
        public void removeObjects()
        {
            GameCanvas.Children.Remove(background);
            GameCanvas.Children.Remove(menu);
        }
    }
}
