// <copyright file="Random.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Intership.Commands
{
    using System;
    using System.Linq;

    /// <summary>
    /// Static Random class for fetching from a range.
    /// </summary>
    public static class Random
    {
       private static System.Random rand = new System.Random();

        /// <summary>
        /// The method returns a random number from the given range with the given exceptions.
        /// </summary>
        /// <param name="rangeMin">First number of range.</param>
        /// <param name="rangeMax">Count of range.</param>
        /// <param name="exclude">Numbers that should not be included in the sample.</param>
        /// <returns>Random number from range whithot other than the given numbers.</returns>
       public static int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)
       {
            var range = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude.Contains(i));

            int index = rand.Next(0, range.Count());
            return range.ElementAt(index);
       }
    }
}
