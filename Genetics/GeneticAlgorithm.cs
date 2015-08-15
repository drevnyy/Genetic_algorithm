using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetics
{
    public class GeneticAlgorithm
    {
        public static Random R = new Random();
        private readonly Func<object, double> _fitnessCalculator;
        private readonly Func<object, double, Random, object> _mutateFunc;
        private readonly Func<object, object, Random, object> _createNextGen;
        private readonly int _maximumChildAmount;
        private readonly int _maximumParentAmount;

        public string FitnessRange = "-";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fitnessCalculator">function that takes objects and returns its fitness</param>
        /// <param name="mutateFunc">function takes object, lowest fitness and class Random and returns mutated object</param>
        /// <param name="createNextGen">function that takes parents and class Random and returns child</param>
        /// <param name="maximumChildAmount">maximal amount of generated childs</param>
        /// <param name="maximumParentAmount">maximal parent pool size</param>
        public GeneticAlgorithm(Func<object, double> fitnessCalculator, Func<object, double, Random, object> mutateFunc, Func<object, object, Random, object> createNextGen, int maximumChildAmount = 0, int maximumParentAmount=2)
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
                var o = _mutateFunc(_createNextGen(parents[R.Next(_maximumParentAmount)], parents[R.Next(_maximumParentAmount)], R),fitnesses.Min(),R);
                childs.Add((T)o);
            }
            
            return childs.ToArray();
        }

        private List<T> GetParents<T>(double[] fitnesses, IReadOnlyList<T> input)
        {
            var added = new List<int>();
            for (int i = 0; i < _maximumParentAmount; i++)
            {
                added.Add(GetIndexOfFitness(fitnesses, added));
            }
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
