using System;
using System.Linq;


namespace Intership.Commands
{
    public static class Random 
    {

       private  static System.Random _rand = new System.Random();

        public static int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)
        {
            var range = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude.Contains(i));

            int index = _rand.Next(0, range.Count());
            return range.ElementAt(index);
        }
    }
}
