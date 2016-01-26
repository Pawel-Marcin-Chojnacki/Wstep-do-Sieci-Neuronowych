using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_hand
{
    public class JednostkaUczaca
    {
        public double[] Wejscie { get; set; } = null;
        public double delta { get; set; }
        public double SumaWejscia { get; set; }
        public double Wyjscie { get; set; }
        public double[] Wagi { get; set; } = null;
        public int IloscWejsc { get; set; }

 
        public void LiczSumeWejscia(double[] wejsciePoprzedniejWarstwy)
        {
            IloscWejsc = wejsciePoprzedniejWarstwy.Length;
            if (Wagi == null)
                LosujWagi(IloscWejsc);

            Wejscie = new double[IloscWejsc] ;
            SumaWejscia = 0;
            for (int i = 0; i < IloscWejsc; i++)
            {
                Wejscie[i] = wejsciePoprzedniejWarstwy[i];
                Debug.Assert(Wagi != null, "Wagi != null");
                SumaWejscia += Wejscie[i]*Wagi[i];
            }
        }

        private void LosujWagi(int length)
        {
            Wagi = new double[length];
            for (int i = 0; i < length; i++)
            {
                Wagi[i] = Globals.Random.NextDouble()-0.5;
            }
        }

        private double Sigma(double d) => 1/(1 + Math.Exp(-d));

        public void ObliczWyjscie() => Wyjscie = Sigma(SumaWejscia);

        public void DeltaWynikowa(double t) => delta = (Wyjscie - t)*Wyjscie*(1 - Wyjscie);

        public void DeltaUkryta(double sigmaDeltaKWkj) => delta = sigmaDeltaKWkj*Wyjscie*(1 - Wyjscie);
    }

    
}
