using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_hand
{
    public class LearningUnit
    {
        public double[] Input { get; set; } = null;
        public double Delta { get; set; }
        public double InputSum { get; set; }
        public double Output { get; set; }
        public double[] Weights { get; set; } = null;
        public int NumberOfInputs { get; set; }

 
        public void CalcInputSum(double[] previousLayerInput)
        {
            NumberOfInputs = previousLayerInput.Length;
            if (Weights == null)
                RandomWeights(NumberOfInputs);

            Input = new double[NumberOfInputs] ;
            InputSum = 0;
            for (int i = 0; i < NumberOfInputs; i++)
            {
                Input[i] = previousLayerInput[i];
                // ReSharper disable once PossibleNullReferenceException
                InputSum += Input[i]*Weights[i];
            }
        }

        private void RandomWeights(int length)
        {
            Weights = new double[length];
            for (int i = 0; i < length; i++)
            {
                Weights[i] = Globals.Random.NextDouble()-0.5;
            }
        }

        private double Sigma(double d) => 1/(1 + Math.Exp(-d));

        public void CalculateOutput() => Output = Sigma(InputSum);

        public void OutputLayerDelta(double t) => Delta = (Output - t)*Output*(1 - Output);

        public void HiddenLayerDelta(double sigmaDeltaKWkj) => Delta = sigmaDeltaKWkj*Output*(1 - Output);
    }

    
}
