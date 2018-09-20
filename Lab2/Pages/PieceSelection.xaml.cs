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
using Lab2.Pages;

namespace Lab2.Pages
{
    /// <summary>
    /// Interaction logic for PieceSelection.xaml
    /// </summary>
    public partial class PieceSelection : Page
    {
        public PieceSelection()
        {
            InitializeComponent();
        }

        public void Select(object sender, RoutedEventArgs e)
        {
            string value = ((Button)sender).Tag.ToString();
            if(value == "X")
            {
                Console.WriteLine("X");
            } else
            {
                Console.WriteLine("O");
            }
            GamePage gamePage = new GamePage();
            this.NavigationService.Navigate(gamePage);
        }
    }
}
