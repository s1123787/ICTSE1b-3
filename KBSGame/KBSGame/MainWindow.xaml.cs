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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KBSGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initStartpoint();
            initEndpoint();
        }

        public void initStartpoint()
        {
            Ellipse cirkel = new Ellipse();
            cirkel.Fill = Brushes.Gray;
            cirkel.Width = 50;
            cirkel.Height = 50;
            Canvas.SetLeft(cirkel, 0);
            Canvas.SetTop(cirkel, 0);
            GameCanvas.Children.Add(cirkel);
        }

        public void initEndpoint()
        {

        }
    }
}
