using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Robot_hand
{
    public struct Robot
    {
        public static Point Shoulder { get; } = Globals.ArmStartingPoint;
        public  Point Elbow { get; set; }
        public  Point Palm { get; set; }

        public static Robot AnglesToRobot(Angles angles)
        {
            Robot arm = new Robot();
            angles.alpha = angles.alpha * Math.PI / 180;
            angles.beta = angles.beta * Math.PI / 180;
            angles.alpha -= Math.PI / 2;
            angles.beta = angles.alpha + angles.beta - Math.PI;

            arm.Elbow = new Point((int)Math.Round(Shoulder.X + Math.Cos(angles.alpha) * Globals.ArmLength), (int)Math.Round(Shoulder.Y + Math.Sin(angles.alpha) * Globals.ArmLength));
            arm.Palm = new Point((int)Math.Round(arm.Elbow.X + Math.Cos(angles.beta) * Globals.ArmLength), (int)Math.Round(arm.Elbow.Y + Math.Sin(angles.beta) * Globals.ArmLength));
            return arm;
        }
    }


}
