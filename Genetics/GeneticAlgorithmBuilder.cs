using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
   public class GeneticAlgorithmBuilder
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="fitnessCalculator">function that takes objects and returns its fitness</param>
      /// <param name="mutateFunc">function takes object, lowest fitness and class Random and returns mutated object</param>
      /// <param name="createNextGen">function that takes parents and class Random and returns child</param>
      /// <param name="maximumChildAmount">maximal amount of generated childs</param>
      /// <param name="maximumParentAmount">maximal parent pool size</param>
      public static GeneticAlgorithm Build(Func<object, double> fitnessCalculator, Func<object, double, object> mutateFunc, Func<object, object, object> createNextGen, int maximumChildAmount = 0, int maximumParentAmount = 2)
      {
         return new GeneticAlgorithm(fitnessCalculator, mutateFunc, createNextGen, maximumChildAmount, maximumParentAmount);
      }
   }
}
