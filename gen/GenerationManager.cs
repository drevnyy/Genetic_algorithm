using System;
using System.Collections.Generic;
using System.Windows.Media;
using Genetics;

namespace gen
{

    internal class GenerationManager
    {
        private GeneticAlgorithm _alg = null;
        public string fitness = "-";
        internal GenerationManager()
        {

        }

        public Color Target { get; set; }

        public Color[] StartNewGenetics(Color[] currentGen)
        {
            _alg = GeneticAlgorithmBuilder.Build(FitnessCalculatorFunc, MutateFunc, CreateNextGenFunc, 9, 5);
            return ContinueGen(currentGen);
        }
        public Color[] ContinueGen(Color[] currentGen)
        {
            if (_alg == null)
                return StartNewGenetics(currentGen);
            var colors = _alg.NextGen(currentGen);
            fitness = _alg.FitnessRange;
            return colors;
        }


        private object CreateNextGenFunc(object o, object o1)
        {
            var childR = ComputeChild(((Color)o).R, ((Color)o1).R);
            var childG = ComputeChild(((Color)o).G, ((Color)o1).G);
            var childB = ComputeChild(((Color)o).B, ((Color)o1).B);

            Color c = new Color
            {
                R = Helper.H.IntToByte(childR),
                G = Helper.H.IntToByte(childG),
                B = Helper.H.IntToByte(childB)
            };
            return c;

        }

        private static int ComputeChild(byte c1, byte c2)
        {
            int mutationIndex = Randomizer.Instance.Next(100);
            var childR = c1 * mutationIndex + c2 * (100 - mutationIndex);
            if (childR % 100 >= 50)
                childR = childR / 100 + 1;
            else
                childR /= 100;
            return childR;
        }

        private object MutateFunc(object arg, double smallestFitness)
        {
            
            int mutationScale = 3;
            if (Randomizer.Instance.Next(1000) < smallestFitness) 
            {
                Color c = new Color
                {
                    R = Helper.H.IntToByte(((Color)arg).R + Randomizer.Instance.Next(-mutationScale, mutationScale)),
                    G = Helper.H.IntToByte(((Color)arg).G + Randomizer.Instance.Next(-mutationScale, mutationScale)),
                    B = Helper.H.IntToByte(((Color)arg).B + Randomizer.Instance.Next(-mutationScale, mutationScale))
                };
                return c;
            }
            return arg;
        }



        private double FitnessCalculatorFunc(object o)
        {
            return Helper.Modulo(Math.Pow(((Color)o).R-Target.R, 2))
                 + Helper.Modulo(Math.Pow(((Color)o).G-Target.G, 2))
                 + Helper.Modulo(Math.Pow(((Color)o).B-Target.B, 2))
                 + Helper.Modulo(Math.Sqrt(((Color)o).R) - Math.Sqrt(Target.R))
                 + Helper.Modulo(Math.Sqrt(((Color)o).G) - Math.Sqrt(Target.G))
                 + Helper.Modulo(Math.Sqrt(((Color)o).B) - Math.Sqrt(Target.B));
        }


    }
}
