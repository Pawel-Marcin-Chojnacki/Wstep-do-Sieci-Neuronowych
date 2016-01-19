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
        public static int GridSize = 35;
        private int[] _displayData = new int[GridSize];
        private List<Button> _buttons = new List<Button>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeDisplay();
            LoadLearningFile();

        }

        private void LoadLearningFile()
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.FileName = "../../tests.txt";
            try
            {
                var fileLines = File.ReadAllLines(fileDialog.FileName);
                for (int i = 0; i < fileLines.Length; i++)
                {
                    Test test = new Test();
                    test.cells = new int[ScreenDim];
                    var splitedLine = fileLines[i].Split(' ');
                    int j;
                    for (j = 0; j < ScreenDim; j++)
                    {
                        test.cells[j] = Convert.ToInt32(splitedLine[j]);
                    }
                    test.anwser = splitedLine[j];
                    _tests.Add(test);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void InitializeDisplay()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _displayData[i * 5 + j] = -1;
                    var rec = new Button()
                    {
                        Background = Brushes.Lavender,
                        Margin = new Thickness(1),
                        Tag = i * 5 + j
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

          //  Check(null, null);
        }

    }
}
