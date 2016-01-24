using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Algorytm_wieżowy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] _displayData = new int[Globals.GridSize];
        private List<Button> _buttons = new List<Button>();
        private List<Sample> _samples = new List<Sample>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeDisplay();
            LoadTests();
            Learn();

        }

        private void Learn()
        {
            for (int i = 0; i < Globals.Digits; ++i)
                TowerAlgorithm(i);
        }

        private void TowerAlgorithm(int i)
        {
            //tower[i] = new Tower();
            //tower[i].Learn();
        }

        private void LoadTests()
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "../../tests.txt"
            };
            try
            {
                var fileLines = File.ReadAllLines(fileDialog.FileName);
                foreach (string line in fileLines)
                {
                    var sample = new Sample
                    {
                        Cells = new int[Globals.GridSize]
                    };
                    var splitedLine = line.Split(' ');
                    int j;
                    for (j = 0; j < Globals.GridSize; j++)
                    {
                        sample.Cells[j] = Convert.ToInt32(splitedLine[j]);
                    }
                    sample.Digit = splitedLine[j];
                    _samples.Add(sample);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void InitializeDisplay()
        {
            for (int i = 0; i < Globals.Height; i++)
            {
                for (int j = 0; j < Globals.Width; j++)
                {
                    _displayData[i * Globals.Width + j] = -1;
                    var rec = new Button()
                    {
                        Background = Brushes.Lavender,
                        Margin = new Thickness(1),
                        Tag = i * Globals.Width + j
                    };
                    rec.Click += Button_Clicked;
                    Grid.SetRow(rec, i);
                    Grid.SetColumn(rec, j);
                    display.Children.Add(rec);
                    _buttons.Add(rec);
                }
            }
        }

        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = Convert.ToInt32(button.Tag);
            _displayData[id] = -_displayData[id];
            if (button.Background.Equals(Brushes.Lavender))
                button.Background = Brushes.RosyBrown;
            else
                button.Background = Brushes.Lavender;

            //RecognizeDigits(null, null);
        }

    }
}
