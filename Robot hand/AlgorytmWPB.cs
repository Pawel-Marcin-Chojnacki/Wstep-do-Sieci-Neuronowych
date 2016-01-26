using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Robot_hand
{
    public class AlgorytmWPB
    {
        public const double n = 0.3; //Współczynnik uczenia.
        public List<JednostkaUczaca[]> Warstwy = new List<JednostkaUczaca[]>();
        public LearningExample[] przykladyUczace;

        public void InitLayers()
        {
            for (int k = 0; k < Globals.UnitsPerLayer.Length; k++)
            {
                Warstwy.Add(new JednostkaUczaca[Globals.UnitsPerLayer[k]]);
                for (int i = 0; i < Warstwy[k].Length; i++)
                    Warstwy[k][i] = new JednostkaUczaca();
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
            for (int i = 0; i < Warstwy.Count; i++)
            {
                for (int j = 0; j < Warstwy[i].Length; j++)
                {
                    for (int k = 0; k < Warstwy[i][j].Wagi.Length; k++)
                    {
                        Warstwy[i][j].Wagi[k] -= n*Warstwy[i][j].delta*Warstwy[i][j].Wejscie[k];
                    }
            
                }
            }
        }

        internal Robot DajOdpowiedź(Point p)
        {
            var znormalizowany = LearningExample.NormalizePoint(p);
            //Debug.WriteLine("punkt na wejściu: {0}, {1}", p.X, p.Y);
            int Ostatnia = Warstwy.Count - 1;

            double[] x = new double[] { znormalizowany.X, znormalizowany.Y, 1 };
            GoForward(x);

            Angles kąty = new Angles(Warstwy[Ostatnia][0].Wyjscie, Warstwy[Ostatnia][1].Wyjscie);
            Debug.WriteLine("wynik: {0}, {1}", kąty.alpha.ToString(), kąty.beta.ToString());
            var zdenormalizowane = LearningExample.DenormalizeAngles(kąty);
            //Debug.WriteLine("wynik: {0}, {1}", kąty.Alfa.ToString(), kąty.Beta.ToString());
            return Robot.AnglesToRobot(zdenormalizowane);
        }

        public void GoForward(double[] x)
        {
            for (int i = 0; i < Warstwy.Count; i++)
            {
                for (int j = 0; j < Warstwy[i].Length; j++)
                {
                    Warstwy[i][j].LiczSumeWejscia(x);
                    Warstwy[i][j].ObliczWyjscie();
                }

                x = new double[Warstwy[i].Length+1];
                for (int j = 0; j < Warstwy[i].Length; j++)
                {
                    x[j] = Warstwy[i][j].Wyjscie;
                }
                x[Warstwy[i].Length] = 1;
            }
        }

        public void GoBackwards(double[] x)
        {
            int resultLayerIndex = Warstwy.Count - 1;
            
            //Oblicz deltę dla ostatniej warstwy (wynikowej)
            for (int i = 0; i < x.Length; i++)
            {
                Warstwy[resultLayerIndex][i].DeltaWynikowa(x[i]);
            }

            for (int i = resultLayerIndex-1; i >=0; i-- )
            {
                for (int j = 0; j < Warstwy[i].Length; j++)
                {
                    double sum = SumOfDeltaK(i, j);
                    Warstwy[i][j].DeltaUkryta(sum);
                }   
            }


        }

        private double SumOfDeltaK(int k, int j)
        {
            double result = 0;
            for (int i = 0; i < Warstwy[k + 1].Length; i++)
            {
                result += Warstwy[k + 1][i].delta*Warstwy[k + 1][i].Wagi[j];
            }
            return result;
        }

        private LearningExample RandomExample()
        {
            return przykladyUczace[Globals.Random.Next(0, przykladyUczace.Length)];
        }

        public void GenerujKaty()
        {
            przykladyUczace = new LearningExample[Globals.LearningExamplesCount];
            for (int i = 0; i < przykladyUczace.Length; i++)
            {
                przykladyUczace[i] = new LearningExample();
            }
        }
    }
}
