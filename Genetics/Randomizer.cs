using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
   public class Randomizer//singleton
   {
      private Random r = new Random();

      public static Randomizer Instance = new Randomizer();

      private Randomizer()
      {

      }
      public int Next(int? x = null, int? max = null)
      {
         if (max.HasValue)
            return r.Next(x.Value, max.Value);
         if (x.HasValue)
            return r.Next(x.Value);
         return r.Next();
      }
   }
}
