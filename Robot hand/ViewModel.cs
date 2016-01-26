using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Robot_hand.Annotations;

namespace Robot_hand
{
    class ViewModel : INotifyPropertyChanged
    {
        private Point _elbow = new Point(Globals.ArmLength, Globals.ArmStartingPoint.Y);
        private Point _palm = new Point(Globals.ArmLength*2, Globals.ArmStartingPoint.Y);
        public Point Shoulder => Globals.ArmStartingPoint;
        public int BorderHeight => Globals.Height;
        public int BorderWidth => Globals.Width;

        public Point Elbow
        {
            get { return _elbow; }
            set
            {
                _elbow = value;
                NotifyPropertyChanged("Elbow");
            }
        }

        public Point Palm
        {
            get { return _palm;}
            set
            {
                _palm = value;
                NotifyPropertyChanged("Palm");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
