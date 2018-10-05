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

namespace KBSGame
{
    /// <summary>
    /// Interaction logic for EndPointModal.xaml
    /// </summary>
    public partial class EndPointModal : Window
    {
        public EndPointModal()
        {
            InitializeComponent();
        }

        private void Again_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        
    }
}
