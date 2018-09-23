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
using Lab2.Classes;

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
            Player player;
            Player computer;
            Random random = new Random();

            Board board = new Board
            {
                PlayArea = new string[3, 3]
            };

            string value = ((Button)sender).Tag.ToString();


            if(value == "X") //Player is X
            {
                player = new Player {
                    IsHuman = true,
                    Piece = "X"
                };
                computer = new Player {
                    IsHuman = false,
                    Piece = "O"
                };
            } else //Player is O
            {
                player = new Player
                {
                    IsHuman = true,
                    Piece = "O"
                };
                computer = new Player
                {
                    IsHuman = false,
                    Piece = "X"
                };
            }


            Game game = new Game {
                Player1 = player,
                Player2 = computer,
                GameBoard = board,
                IsPlayerTurn = random.Next(2) < 2
            };

            GamePage gamePage = new GamePage(game);
            this.NavigationService.Navigate(gamePage);
        }
    }
}
