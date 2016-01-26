using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Robot_hand
{
    public class AlgorithmWPB
    {
        private const double N = 0.3; //Współczynnik uczenia.
        private List<LearningUnit[]> _layers = new List<LearningUnit[]>();
        private LearningExample[] _learningExamples;

        public void InitLayers()
        {
            for (int k = 0; k < Globals.UnitsPerLayer.Length; k++)
            {
                _layers.Add(new LearningUnit[Globals.UnitsPerLayer[k]]);
                for (int i = 0; i < _layers[k].Length; i++)
                    _layers[k][i] = new LearningUnit();
            }
        }

        public void Teach()
        {
            for (int i = 0; i < Globals.LearningSteps; i++)
            {
                LearningExample example = RandomExample();
                double[] randomInput = {example.Point.X, example.Point.Y, 1};
                GoForward(randomInput);

                double[] generatedOutput = {example.Angles.alpha, example.Angles.beta};
                GoBackwards(generatedOutput);

                UpdateWeights();
            }
        }

        private void UpdateWeights()
        {
            foreach (LearningUnit[] layer in _layers) //Run through every layer.          
                foreach (LearningUnit learningUnit in layer) //Foreach unit.               
                    for (int k = 0; k < learningUnit.Weights.Length; k++) //update every weight in unit
                        learningUnit.Weights[k] -= N*learningUnit.Delta*learningUnit.Input[k];
        }

        internal Robot GetArmPosition(Point p)
        {
            var normalizedPoint = LearningExample.NormalizePoint(p);
            int LastLayer = _layers.Count - 1;

            double[] x = { normalizedPoint.X, normalizedPoint.Y, 1 };
            GoForward(x);

            Angles angles = new Angles(_layers[LastLayer][0].Output, _layers[LastLayer][1].Output);
            var denormalizedAngles = LearningExample.DenormalizeAngles(angles);
            return Robot.AnglesToRobot(denormalizedAngles);
        }

        public void GoForward(double[] x)
        {
            for (int i = 0; i < _layers.Count; i++)
            {
                for (int j = 0; j < _layers[i].Length; j++)
                {
                    _layers[i][j].CalcInputSum(x);
                    _layers[i][j].CalculateOutput();
                }

                x = new double[_layers[i].Length+1];
                for (int j = 0; j < _layers[i].Length; j++)
                {
                    x[j] = _layers[i][j].Output;
                }
                x[_layers[i].Length] = 1;
            }
        }

        public void GoBackwards(double[] x)
        {
            int resultLayerIndex = _layers.Count - 1;
            
            //Oblicz deltę dla ostatniej warstwy (wynikowej)
            for (int i = 0; i < x.Length; i++)
            {
                _layers[resultLayerIndex][i].OutputLayerDelta(x[i]);
            }

            for (int i = resultLayerIndex-1; i >=0; i-- )
            {
                for (int j = 0; j < _layers[i].Length; j++)
                {
                    double sum = SumOfDeltaK(i, j);
                    _layers[i][j].HiddenLayerDelta(sum);
                }   
            }


        }

        private double SumOfDeltaK(int k, int j)
        {
            double result = 0;
            for (int i = 0; i < _layers[k + 1].Length; i++)
            {
                result += _layers[k + 1][i].Delta*_layers[k + 1][i].Weights[j];
            }
            return result;
        }

        private LearningExample RandomExample()
        {
            return _learningExamples[Globals.Random.Next(0, _learningExamples.Length)];
        }

        public void InitializeExamples()
        {
            _learningExamples = new LearningExample[Globals.LearningExamplesCount];
            for (int i = 0; i < _learningExamples.Length; i++)
            {
                _learningExamples[i] = new LearningExample();
            }
        }
    }
}
