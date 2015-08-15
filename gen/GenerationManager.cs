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
            _alg = new GeneticAlgorithm(FitnessCalculatorFunc, MutateFunc, CreateNextGenFunc, 9, 5);
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


        private object CreateNextGenFunc(object o, object o1, Random r)
        {
            var childR = ComputeChild(((Color)o).R, ((Color)o1).R, r);
            var childG = ComputeChild(((Color)o).G, ((Color)o1).G, r);
            var childB = ComputeChild(((Color)o).B, ((Color)o1).B, r);

            Color c = new Color
            {
                R = Helper.IntToByte(childR),
                G = Helper.IntToByte(childG),
                B = Helper.IntToByte(childB)
            };
            return c;

        }

        private static int ComputeChild(byte c1, byte c2, Random r)
        {
            int mutationIndex = r.Next(100);
            var childR = c1 * mutationIndex + c2 * (100 - mutationIndex);
            if (childR % 100 >= 50)
                childR = childR / 100 + 1;
            else
                childR /= 100;
            return childR;
        }

        private object MutateFunc(object arg, double smallestFitness, Random r)
        {
            //TODO: consider changing this field to dynamic
            int mutationScale = 3;
            if (r.Next(1000) < smallestFitness) //the more stable the smallest chance of mutation
            {
                Color c = new Color
                {
                    R = Helper.IntToByte(((Color)arg).R + r.Next(-mutationScale, mutationScale)),
                    G = Helper.IntToByte(((Color)arg).G + r.Next(-mutationScale, mutationScale)),
                    B = Helper.IntToByte(((Color)arg).B + r.Next(-mutationScale, mutationScale))
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
