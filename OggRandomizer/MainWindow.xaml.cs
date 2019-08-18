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
using System.IO;

namespace OggRandomizer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Randomize_Button_Click(object sender, RoutedEventArgs e)
        {
            if (FolderPath_Box.Text != "Folder")
            {
                List<string> allOGGs = System.IO.Directory.GetFiles(FolderPath_Box.Text, "*.ogg", SearchOption.AllDirectories).ToList<string>();

                for (int i = 0; i < allOGGs.Count();)
                {
                    int FileAIndex = rnd.Next(0, allOGGs.Count()); //Store the index number of file B because this is the file we are gonna randomize, so we gotta remove it from the array afterwards
                    string FileA = allOGGs[FileAIndex]; //File to which we want to take the sprites from
                    int FileBIndex = rnd.Next(0, allOGGs.Count()); //Store the index number of file B because this is the file we are gonna randomize, so we gotta remove it from the array afterwards
                    string FileB = allOGGs[FileBIndex]; // File in which we want to inject the sprites into

                    if (FileAIndex != FileBIndex) // Dont move if file is the same 
                    {
                        File.Move(FileA, FileA + ".tmp");
                        File.Move(FileB, FileA);
                        File.Move(FileA + ".tmp", FileB);
                    }

                    //Remove the file with the higher index first so the other files index doesn't get affected
                    if (FileAIndex > FileBIndex)
                    {
                        allOGGs.RemoveAt(FileAIndex);
                        allOGGs.RemoveAt(FileBIndex);
                    }
                    else if (FileAIndex != FileBIndex) // Only remove the file once if the index was identical
                    {
                        allOGGs.RemoveAt(FileBIndex);
                        allOGGs.RemoveAt(FileAIndex);
                    }
                    else
                    {
                        allOGGs.RemoveAt(FileAIndex);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a folder first");
            }
        }

        private void SelectFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FolderPath_Box.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
