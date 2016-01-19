using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Algorytm_wieżowy
{
    class Weights
    {
        public double[] weights;
        public int Count { get; set; }
        public Random rnd = new Random();
        public int LifeTime { get; set; }

        public Weights(int size)
        {
            Count = size;
            weights = new double[Count];
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < Count; ++i)
            {
                weights[i] = (double) rnd.Next(1, 100)/100;
                LifeTime = 0;
            }
        }


    }
}
