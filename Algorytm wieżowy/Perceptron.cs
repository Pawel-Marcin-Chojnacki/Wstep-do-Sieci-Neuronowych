using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Algorytm_wieżowy
{
    class Perceptron
    {
        public int Id { get; set; }
        public double Threshold { get; set; }
        public double ThresholdRecord { get; set; }
        public double LearningRate { get; set; }
        public Weights Weights { get; set; }
        public Weights Pocket { get; set; }
        public int Size { get; set; }
        public bool IsSamplePositive = false;
        public double Error { get; set; }
        public double O { get; set; }
        //public List<Sample> Samples; 

        public Perceptron(int number, int size)
        {
            Id = number;
            Size = size;
            Weights = new Weights(Size);
            Pocket = new Weights(Size);
            Threshold = 0;
            Error = 0;
            O = 0;
        }

        public void UpdateThreshold(double error)
        {
            Threshold = Threshold - LearningRate * error;
        }

        public void Learn(Sample sample)
        {
            O = 0;

        }

    }


}
