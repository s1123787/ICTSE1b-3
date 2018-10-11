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
        public MainMenu mm { get; set; }
        public MainMenu()
        {
            mm = this;
            InitializeComponent();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Opens a new window and starts the game
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Closes the application
            Application.Current.Shutdown();
        }
    }
}
