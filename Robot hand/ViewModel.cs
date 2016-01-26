using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Robot_hand.Annotations;

namespace Robot_hand
{
    class ViewModel : INotifyPropertyChanged
    {
        private Point _lokiec = new Point(Globals.ArmLength, Globals.ArmStartingPoint.Y);
        private Point _dlon = new Point(Globals.ArmLength*2, Globals.ArmStartingPoint.Y);
        public Point PoczatekRamienia => Globals.ArmStartingPoint;
        public int BorderHeight => Globals.Height;
        public int BorderWidth => Globals.Width;

        public Point Lokiec
        {
            get { return _lokiec; }
            set
            {
                _lokiec = value;
                NotifyPropertyChanged("Lokiec");
            }
        }

        public Point Dlon
        {
            get { return _dlon;}
            set
            {
                _dlon = value;
                NotifyPropertyChanged("Dlon");
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
