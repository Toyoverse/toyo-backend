using System;
using BackendToyo.Models;

namespace BackendToyo.Middleware
{
    public static class raffle
    {
        public static SortViewModel main(bool fortified = false)
        { 
            int sort = rnd(0,1000);
            int raridade = 0;
            int toyoRaridade = 0;

            if(fortified) {
                if (sort <= 5) 
                    raridade = 6;
                else if (sort <= 20)
                    raridade = 5;
                else if (sort <= 60)
                    raridade = 4;
                else if (sort <= 160)
                    raridade = 3;
                else if (sort <= 480)
                    raridade = 2;
                else
                    raridade = 1;
            } else {
                if (sort <= 25)
                    raridade = 6;
                else if (sort <= 80)
                    raridade = 5;
                else if (sort <= 280)
                    raridade = 4;
                else
                    raridade = 3;
            }

            if (raridade == 1 || raridade == 2)
                toyoRaridade = raridade;
            else if (raridade == 3)
                if (rnd(0,100) <= 50)
                    toyoRaridade = 3;
                else
                    toyoRaridade = 4;
            else if (raridade >= 4)
                toyoRaridade = raridade + 1;
            
            int qRari = raridade;

            int nRnd = rnd(0, 1000);
            int min = qRari * 4 + 16; //20 - 40

            int[] sOrd = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            int tempGO = 0;
            for (int i = 0; i < sOrd.Length; i++)
            {
                int rando = rnd(0, sOrd.Length - 1);
                tempGO = sOrd[i];
                sOrd[i] = sOrd[rando];
                sOrd[rando] = tempGO;
            }

            int[] sMax = new int[] { 0, 120, 120, 120, 120, 96, 96, 64, 64, 40, 40, 40, 40 };
            int[] sMin = new int[] { 0, min * 2, min * 2, min * 2, min * 2, (int)(min * 1.5f), (int)(min * 1.5f), (int)(min * 0.75f), (int)(min * 0.75f), (int)(min * 0.5f), (int)(min * 0.5f), (int)(min * 0.5f), (int)(min * 0.5f) };

            //0, 1:VITALITY, 2:STRENGTH, 3:RESISTANCE, 4:CYBER, 5:RESILIENCE, 6:PRECISION , 7:TECHNIQUE, 8:ANALYSIS, 9:SPEED, 10:AGILITY, 11:STAMINA, 12:LUCK
            int[][] oqParts = new int[][] { new int[] { 0, 0 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new int[] { 0, 0 }, new int[] { 3, 11, 6 }, new int[] { 3, 11, 6 }, new int[] { 2, 4, 10 }, new int[] { 2, 4, 10 }, new int[] { 1, 3, 8 }, new int[] { 1, 3, 8 }, new int[] { 7, 9, 12 }, new int[] { 7, 9, 12 } };
            int[] qStats = new int[] { 0, rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100), rnd(min, 100) };
            for (int i = 0; i < sOrd.Length; i++)
            {
                int o = sOrd[i];
                qStats[o] = (int)(rnd(sMin[i + 1], sMax[i + 1]));
            }

            int[][] qParts = new int[][] {
                new int[] { 0, 0 },
                new int[] { oqParts[1][rnd(0, oqParts[1].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { 0, 0 },
                new int[] { oqParts[3][rnd(0, oqParts[3].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[4][rnd(0, oqParts[4].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[5][rnd(0, oqParts[5].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[6][rnd(0, oqParts[6].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[7][rnd(0, oqParts[7].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[8][rnd(0, oqParts[8].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[9][rnd(0, oqParts[9].Length - 1)], rnd(min / 5 -1, 10) },
                new int[] { oqParts[10][rnd(0, oqParts[10].Length - 1)], rnd(min / 5 -1, 10) }
            };

            SortViewModel svm = new SortViewModel();
            svm.oqParts = oqParts;
            svm.qStats = qStats;
            svm.toyoRaridade = toyoRaridade;
            svm.raridade = raridade;

            return svm;
        }
        public static int rnd(int _min, int _max, int but = -999) {

            long seed = preRnd(0, 1000);

            long a = preRnd(1100515245, 1103515245);
            //increment
            long c = preRnd(10345, 12345);
            //modulus m (which is also the maximum possible random value)
            long m = (long)MathF.Pow(2f, 31f);

            //To display the random numbers when the loop is finished
            string allRandomNumbers = "";

            //How many random numbers do we want to generate?
            int amountOfNumbers = 733;

            //Array that will store the random numbers so we can display them
            float[] randomNumbers = new float[amountOfNumbers];

            for(int i = 0; i < amountOfNumbers; i++) {
                //Basic idea: seed = (a * seed + c) mod m
                seed = (a * seed + c) % m;

                //To get a value between 0 and 1
                float randomValue = seed / (float)m;

                //Remove this line if you want to speed up the testing of the algorithm
                float minMax = _max + 0.99999f - _min;
                int valFin = (int)(minMax * randomValue) + _min;
                allRandomNumbers += valFin + " ";

                randomNumbers[i] = valFin;
            }

            int qual = preRnd(0, 732);
            int saida = (int)randomNumbers[qual];

            if(saida == but)
                saida = (int)randomNumbers[qual + 1];
            if(qual >= amountOfNumbers)
                saida = (int)randomNumbers[1];

            return saida;

        }

        public static int preRnd(int n1, int n2, int but = -1) {
            int saida = 0;

            var random = new Random(Guid.NewGuid().GetHashCode());

            saida = random.Next(n1, n2 + 1);

            if(saida == but)
                saida++;
            if(saida > n2 + 1)
                saida = n1;

            return saida;
        }
    }
}