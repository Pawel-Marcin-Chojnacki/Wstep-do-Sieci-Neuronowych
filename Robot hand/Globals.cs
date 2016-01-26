using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Robot_hand
{
    public static class Globals
    {
        public static int Height { get; } = 400;
        public static int Width { get; } = 400;
        public static int LearningExamplesCount = 10000;
        public static int[] UnitsPerLayer = {4,4,4, 2};
        // ReSharper disable once PossibleLossOfFraction
        public static Point ArmStartingPoint { get; } = new Point(0, Height / 2);
        public static int LearningSteps { get; } = 1000000;
        public static Random Random = new Random();
        public static int ArmLength = 80;
    }
}
