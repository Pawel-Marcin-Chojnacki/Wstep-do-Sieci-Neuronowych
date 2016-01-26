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

        AlgorithmWPB _algorithmWPB;
        private ViewModel _vm;
        //Point previousPoint;
        public MainWindow()
        {
            InitializeComponent();
            _vm = this.DataContext as ViewModel;
            _algorithmWPB = new AlgorithmWPB();
            _algorithmWPB.InitLayers();
            _algorithmWPB.InitializeExamples();
            _algorithmWPB.Teach();
        }

        private void MoveArm(Point punkt)
        {
            Robot arm = _algorithmWPB.GetArmPosition(punkt);
            _vm.Elbow = arm.Elbow;
            _vm.Palm = arm.Palm;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var point = Mouse.GetPosition((Canvas)sender);
            if (point.X >= 0 && point.X <= Globals.Width &&
            point.Y >= 0 && point.Y <= Globals.Height)
                MoveArm(point);
        }
    }
}
