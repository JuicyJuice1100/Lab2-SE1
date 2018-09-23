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
                        GamePage gamePage = new GamePage(game);
                        //LoadBoard(gamePage);
                        gamePage.LoadBoard();
                        gamePage.InfoBox.Text = "Game Loaded";
                        this.NavigationService.Navigate(gamePage);
                    }
                    catch (IOException exception)
                    {
                        Console.WriteLine(exception.Message);
                        Console.WriteLine("Failed to Load Game");
                    }
                }
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //private void LoadBoard(GamePage gamePage)
        //{
        //    //TODO: This is ugly and inefficient.  Need to fix to be quicker and cleaner
        //    gamePage.TopXLeft.Content = gamePage.CurrentGame.GameBoard.PlayArea[0, 0];
        //    gamePage.TopXMiddle.Content = gamePage.CurrentGame.GameBoard.PlayArea[0, 1];
        //    gamePage.TopXRight.Content = gamePage.CurrentGame.GameBoard.PlayArea[0, 2];
        //    gamePage.CenterXLeft.Content = gamePage.CurrentGame.GameBoard.PlayArea[1, 0];
        //    gamePage.CenterXMiddle.Content = gamePage.CurrentGame.GameBoard.PlayArea[1, 1];
        //    gamePage.CenterXRight.Content = gamePage.CurrentGame.GameBoard.PlayArea[1, 2];
        //    gamePage.BottomXLeft.Content = gamePage.CurrentGame.GameBoard.PlayArea[2, 0];
        //    gamePage.BottomXMiddle.Content = gamePage.CurrentGame.GameBoard.PlayArea[2, 1];
        //    gamePage.BottomXRight.Content = gamePage.CurrentGame.GameBoard.PlayArea[2, 2];
        //}
    }
}
