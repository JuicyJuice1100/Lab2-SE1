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
using System.Windows.Forms;
using Newtonsoft.Json;
using Lab2.Classes;
using Lab2.Pages;
using System.IO;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            PieceSelection pieceSelection = new PieceSelection();
            this.NavigationService.Navigate(pieceSelection);
        }

        private void LoadGame(object sender, RoutedEventArgs e)
        {
            string fileName = null;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    try
                    {
                        Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(fileName));
                        //TODO: need to pass json data to game object and load to gamePage
                    }
                    catch (IOException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
            Console.WriteLine("Game Loaded");
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
