using KBSGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KBSGame.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private bool randomLevel = true;
        public MainWindow mainWindow; 

        public MainMenu()
        {
            InitializeComponent();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {

            //Opens a new window and starts the game
            Obstacles.waardes.Clear();
            Obstacles.obstacles.Clear();
            mainWindow = new MainWindow(randomLevel);
            mainWindow.Show();
            this.Close();
        }

        private void exitGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Closes the application
            Application.Current.Shutdown();
        }

        private void randomLevelButton_Click(object sender, RoutedEventArgs e)
        {
            randomLevelButton.Opacity = 1;
            setLevelButton.Opacity = 0.5;
            randomLevel = true;
        }

        private void setLevelButton_Click(object sender, RoutedEventArgs e)
        {
            setLevelButton.Opacity = 1;
            randomLevelButton.Opacity = 0.5;
            randomLevel = false;
        }
    }
}
