using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Genetics
{
    public class GeneticAlgorithm
    {
        private readonly Func<object, double> _fitnessCalculator;
        private readonly Func<object, double, object> _mutateFunc;
        private readonly Func<object, object, object> _createNextGen;
        private readonly int _maximumChildAmount;
        private readonly int _maximumParentAmount;

        public string FitnessRange = "-";
     
        internal GeneticAlgorithm(Func<object, double> fitnessCalculator, Func<object, double, object> mutateFunc, Func<object, object, object> createNextGen, int maximumChildAmount , int maximumParentAmount)
        {
            _fitnessCalculator = fitnessCalculator;
            _mutateFunc = mutateFunc;
            _createNextGen = createNextGen;
            _maximumChildAmount = maximumChildAmount;
            _maximumParentAmount = maximumParentAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="input">current generation</param>
        /// <returns>next generation of population</returns>
      public T[] NextGen<T>(T[] input)
        {
            double[] fitnesses = CountFitnesses(input);
            FitnessRange = fitnesses.Min()+" - "+fitnesses.Max();
            T[] parents = GetParents(fitnesses, input).ToArray();
            List<T> childs= new List<T>();

            for (int i = 0; i < _maximumChildAmount; i++)
            {
                var o = _mutateFunc(_createNextGen(parents[Randomizer.Instance.Next(_maximumParentAmount)], parents[Randomizer.Instance.Next(_maximumParentAmount)]),fitnesses.Min());
                childs.Add((T)o);
            }
            
            return childs.ToArray();
        }

        private List<T> GetParents<T>(double[] fitnesses, IReadOnlyList<T> input)
        {
            var added = new List<int>();
            Parallel.For (0, _maximumParentAmount, index =>
            {
                added.Add(GetIndexOfFitness(fitnesses, added));
            });
            return added.Select(i => input[i]).ToList();
        }

        private int GetIndexOfFitness(double[] fitnesses, List<int> skip)
        {
            double value = int.MaxValue;
            int index = 0;
            for (int i = 0; i < fitnesses.Length; i++)
            {
                if (fitnesses[i] < value && !skip.Contains(i))
                {
                    value = fitnesses[i];
                    index = i;
                }
            }
            return index;
        }

        private double[] CountFitnesses<T>(T[] input)
        {
            double[] fitnesses = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                fitnesses[i] = _fitnessCalculator(input[i]);
            }
            return fitnesses;
        }
    }
}
