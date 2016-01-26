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

namespace Robot_hand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AlgorytmWPB algorytmWpb;
        ViewModel vm;
        Point previousPoint;
        public MainWindow()
        {
            InitializeComponent();
            vm = this.DataContext as ViewModel;
            algorytmWpb = new AlgorytmWPB();
            algorytmWpb.InitLayers();
            algorytmWpb.GenerujKaty();
            algorytmWpb.Teach();
            //MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
       
        }

        private void ruszReke(Point punkt)
        {
            Robot ręka = algorytmWpb.DajOdpowiedź(punkt);
            vm.Lokiec = ręka.Elbow;
            vm.Dlon = ręka.Palm;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var punkt = Mouse.GetPosition((Canvas)sender);
            //if (punkt.X >= 0 && punkt.X <= Globals.Width &&
                //punkt.Y >= 0 && punkt.Y <= Globals.Height)
                ruszReke(punkt);
        }
    }
}
