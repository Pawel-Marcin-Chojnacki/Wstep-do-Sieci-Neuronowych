using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Robot_hand
{
    public class LearningExample
    {
        public Point Point;
        public Angles Angles;
        private static double ScaleFactorX = (0.9 - 0.1)/Globals.Width;
        private static double ScaleFactorY = (0.9 - 0.1)/Globals.Height;
        private static double ScaleFactorAngle = (0.9 - 0.1)/180;
        public LearningExample()
        {
            Angles = new Angles(Globals.Random.NextDouble() * 180, Globals.Random.NextDouble() * 180);
            Robot arm = Robot.AnglesToRobot(Angles);
            Point = NormalizePoint(arm.Palm);
            Angles = NormalizeAngles(Angles);
        }

        public static Angles NormalizeAngles(Angles angles)
        {
            angles.alpha = angles.alpha*ScaleFactorAngle + 0.1;
            angles.beta = angles.beta*ScaleFactorAngle + 0.1;
            return angles;
        }

        public static Point NormalizePoint(Point palm)
        {
            palm.X = palm.X*ScaleFactorX+ 0.1;
            palm.Y = palm.Y*ScaleFactorY + 0.1;
            return palm;
        }

        public static Angles DenormalizeAngles(Angles angles)
        {
            angles.alpha = (angles.alpha - 0.1)/ScaleFactorAngle;
            angles.beta = (angles.beta - 0.1)/ScaleFactorAngle;
            return angles;
        }
    }


}
