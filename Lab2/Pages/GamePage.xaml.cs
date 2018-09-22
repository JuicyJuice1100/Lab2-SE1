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
            //TODO: Create new game object and set defaults

            CurrentGame = game;
        }

        public void BoardClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clicked");
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

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
