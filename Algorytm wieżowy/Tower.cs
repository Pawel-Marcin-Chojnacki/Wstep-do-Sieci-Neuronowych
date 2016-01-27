using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Algorytm_wieżowy
{
    internal class Tower
    {
        public List<Perceptron> perceptrons;
        public List<Sample> samples;
        public Random random = new Random();
        public int Number;

        public Tower(List<Sample> samplesList, int number)
        {
            samples = samplesList;
            Number = number;
            perceptrons = new List<Perceptron>
            {
                new Perceptron(number, Globals.GridSize)
            };
        }

        public void Learn()
        {
            for (int i = 0; i < 50; i++)
            {
                Sample sample = samples[random.Next(0, samples.Count)];
                //perceptrons[0].SingleLearn(sample);
                for (int j = 0; j < perceptrons.Count; ++j)
                {
                    if (j == 0)
                    {
                        
                    }
                }
            }
            
        }

    }
}
