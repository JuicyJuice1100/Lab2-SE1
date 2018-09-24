using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab2.Classes;
using Button = System.Windows.Controls.Button;

//TODO: Insert Game Logic with on click
//TODO: Create WinCheck
//TODO: Create WinPage/Info
namespace Lab2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public Game CurrentGame { get; set; }

        public GamePage(Game game)
        {
            InitializeComponent();

            CurrentGame = game;
            if (game.IsPlayerTurn)
            {
                InfoBox.Text = "Player Turn";
            } else
            {
                do
                {
                    SavePlayArea();
                } while (!RandomMove());
            }
        }

        /// <summary>
        /// Function when a button on the board is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BoardClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e.Source is Button source && !source.HasContent)
                {
                    //TODO: clean this code to be more dynamic.  This switch statement is ugly just don't have the time to fix this atm.
                    //TODO: instead of setting content should just bind models to the buttons.  Just currently dont have the time.
                    //Checks to see what was clicked and updates the board accordingly 
                    switch (source.Name)
                    {
                        case "TopXLeft":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "TopXMiddle":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "TopXRight":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "CenterXLeft":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "CenterXMiddle":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "CenterXRight":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "BottomXLeft":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "BottomXMiddle":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                        case "BottomXRight":
                            source.Content = CurrentGame.Player1.Piece;
                            source.IsEnabled = false;
                            break;
                    }

                    SavePlayArea();

                    if (WinCheck(CurrentGame.Player1.Piece)){
                        InfoBox.Text = "You Win";
                        SaveButton.IsEnabled = false;
                        return;
                    }

                    CurrentGame.MovesTaken++;
                    //Computer will randomly do a move until valid
                    if(CurrentGame.MovesTaken < 9)
                    {
                        do
                        {
                            SavePlayArea();
                        } while (!RandomMove());
                    }

                    if (WinCheck(CurrentGame.Player2.Piece))
                    {
                        InfoBox.Text = "You Lose";
                        SaveButton.IsEnabled = false;
                        return;
                    }

                }
            } catch (Exception exception)
            {
                InfoBox.Text = "Error.  Please Try Again.";
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// When Save button is clicked it will allow the user to save the current game as a json text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Save(object sender, RoutedEventArgs e)
        {
            string fileName = null;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    try
                    {
                        //TODO: change save logic as this points to the page, need to change to point to a game object
                        SavePlayArea();
                        string json = JsonConvert.SerializeObject(CurrentGame);
                        File.WriteAllText(fileName, json);
                        InfoBox.Text = "Game Saved";
                    }
                    catch (IOException exception)
                    {
                        Console.WriteLine(exception.Message);
                        InfoBox.Text = "Unable to Save Game";
                    }
                }
            }
            
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Saves the board to the CurrentGame
        /// </summary>
        public void SavePlayArea()
        {
            //TODO: Clean this up.  I find this code ugly.
            CurrentGame.GameBoard.PlayArea[0, 0] = TopXLeft.HasContent ? SetButtonAttributes(TopXLeft): null;
            CurrentGame.GameBoard.PlayArea[0, 1] = TopXMiddle.HasContent ? SetButtonAttributes(TopXMiddle) : null;
            CurrentGame.GameBoard.PlayArea[0, 2] = TopXRight.HasContent ? SetButtonAttributes(TopXRight) : null;
            CurrentGame.GameBoard.PlayArea[1, 0] = CenterXLeft.HasContent ? SetButtonAttributes(CenterXLeft) : null;
            CurrentGame.GameBoard.PlayArea[1, 1] = CenterXMiddle.HasContent ? SetButtonAttributes(CenterXMiddle) : null;
            CurrentGame.GameBoard.PlayArea[1, 2] = CenterXRight.HasContent ? SetButtonAttributes(CenterXRight) : null;
            CurrentGame.GameBoard.PlayArea[2, 0] = BottomXLeft.HasContent ? SetButtonAttributes(BottomXLeft) : null;
            CurrentGame.GameBoard.PlayArea[2, 1] = BottomXMiddle.HasContent ? SetButtonAttributes(BottomXMiddle) : null;
            CurrentGame.GameBoard.PlayArea[2, 2] = BottomXRight.HasContent ? SetButtonAttributes(BottomXRight) : null;
        }

        /// <summary>
        /// this will disable the button and set the PlayArea button to a string
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private string SetButtonAttributes(Button button)
        {
            button.IsEnabled = false;
            return button.Content.ToString();
        }


        /// <summary>
        /// Sets the gui Board according to the CurrentGame
        /// </summary>
        /// <param name="gamePage"></param>
        public void LoadBoard()
        {
            //TODO: This is ugly and inefficient.  Need to fix to be quicker and cleaner
            TopXLeft.Content = CurrentGame.GameBoard.PlayArea[0, 0];
            TopXMiddle.Content = CurrentGame.GameBoard.PlayArea[0, 1];
            TopXRight.Content = CurrentGame.GameBoard.PlayArea[0, 2];
            CenterXLeft.Content = CurrentGame.GameBoard.PlayArea[1, 0];
            CenterXMiddle.Content = CurrentGame.GameBoard.PlayArea[1, 1];
            CenterXRight.Content = CurrentGame.GameBoard.PlayArea[1, 2];
            BottomXLeft.Content = CurrentGame.GameBoard.PlayArea[2, 0];
            BottomXMiddle.Content = CurrentGame.GameBoard.PlayArea[2, 1];
            BottomXRight.Content = CurrentGame.GameBoard.PlayArea[2, 2];
        }

        /// <summary>
        /// This will make a random move for the computer
        /// </summary>
        /// <returns></returns>
        /// TODO: Should be moved to the Board.class but ran out of time to fix that logic.
        public bool RandomMove()
        {
            Random random = new Random();
            return InsertMove(random.Next(3), random.Next(3), CurrentGame.Player2.Piece);
        }


        /// <summary>
        /// This is logic grabbed from my lab1 that I did not change.  Really is only used for the computer to make a valid move.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        /// TODO: Should be moved to the Board.class but ran out of time to fix that logic.
        public bool InsertMove(int row, int column, string move)
        {
            InfoBox.Text = "Computer is Making a move";
            if ((row < 0 || row > 2) && (column < 0 || column > 2))
            {
                return false;
            }
            else if (CurrentGame.GameBoard.PlayArea[row, column] != null)
            {
                return false;
            }
            else
            {
                CurrentGame.GameBoard.PlayArea[row, column] = move;
                LoadBoard();
                InfoBox.Text = "Player Turn";
                CurrentGame.MovesTaken++;
                if(CurrentGame.MovesTaken >= 9)
                {
                    SaveButton.IsEnabled = false;
                    //TODO: Replace with win check function
                    InfoBox.Text = "Game Finished";
                }
                return true;
            }
        }

        /// <summary>
        /// Function that will check if player1 or player2 got 3 in a row
        /// </summary>
        /// <returns></returns>
        private bool WinCheck(string piece)
        {
            //TODO: This can be written better for better efficiency.  Just didn't have time to implement.
            bool isWin = false;
            for(int i = 0; i < 3; i++) //TODO: Make this more dynamic.  Should avoid hardcoding values.
            {
                string[] row = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    row[j] = CurrentGame.GameBoard.PlayArea[i, j];
                    if(row[j] == null) { break; } //If there is nothing in the array then we should stop checking that array
                }
                isWin = row.All(s => string.Equals(piece, s));  //looks at the first item and compares the rest of the array
                if (isWin) { return isWin; }
            }

            //TODO: This can be written better for better efficiency.  Just didn't have time to implement.
            for (int i = 0; i < 3; i++) //TODO: Make this more dynamic.  Should avoid hardcoding values.
            {
                string[] column = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    column[j] = CurrentGame.GameBoard.PlayArea[j, i];
                    if (column[j] == null) { break; } //If there is nothing in the array then we should stop checking that array
                }
                isWin = column.All(s => string.Equals(piece, s));  //looks at the first item and compares the rest of the array
                if (isWin) { return isWin; }
            }

            //TODO: This can be written better for better efficiency.  Just didn't have time to implement.
            for (int i = 0; i < 3; i++) //TODO: make this more dynamic
            {
                string[] diagonalRight = new string[3];
                string[] diagonalLeft = new string[3];

                diagonalRight[i] = CurrentGame.GameBoard.PlayArea[i, i];
                diagonalLeft[i] = CurrentGame.GameBoard.PlayArea[i, 2 - i];

                if (diagonalRight[i] == null && diagonalLeft[i] == null) { break; } //If there is nothing in the array then we should stop checking that array


                isWin = diagonalRight.All(s => string.Equals(piece, s));  //looks at the first item and compares the rest of the array
                isWin = diagonalLeft.All(s => string.Equals(piece, s));  //looks at the first item and compares the rest of the array

                if (isWin) { return isWin; }
            }

            return isWin;
        }
    }
}
