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
        private int MovesTaken { get; set; } //number of moves taken already.  This will increment by 1 after every move is taken

        public GamePage(Game game)
        {
            InitializeComponent();
            //TODO: Create new game object and set defaults

            CurrentGame = game;
            MovesTaken = 0;
            if (game.IsPlayerTurn)
            {
                InfoBox.Text = "Player Turn";
            } else
            {
                RandomMove();
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

                    MovesTaken++;
                    //Computer will randomly do a move until valid
                    if(MovesTaken < 9)
                    {
                        do
                        {
                            SavePlayArea();
                            InfoBox.Text = "Computer is Making a move";
                        } while (!RandomMove());

                        LoadBoard();
                        InfoBox.Text = "Player Turn";
                        
                    } else
                    {
                        InfoBox.Text = "Game Finished";
                    }
                    

                }
            } catch (Exception exception)
            {
                InfoBox.Text = "Error.  Please Try Again.";
                Console.WriteLine(exception.Message);
            }
        }

        public void Save(object sender, RoutedEventArgs e)
        {
            //TODO: Export to a text file
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

        public bool RandomMove()
        {
            Random random = new Random();
            return InsertMove(random.Next(3), random.Next(3), CurrentGame.Player2.Piece);
        }

        public bool InsertMove(int row, int column, string move)
        {
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
                MovesTaken++;
                return true;
            }
        }
    }
}
