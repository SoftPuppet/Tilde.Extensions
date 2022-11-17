using System;
using System.Collections.Generic;
using System.Text;

namespace Tilde.Extensions.Utilities
{
    public static class FuzzyCompare
    {
        public static int Distance(string string1, string string2)
        {
            if (string2.Length == 0) {
                Console.WriteLine("string2.Length = 0, string1.Length = " + string1.Length);
                return string1.Length;
            }

            int[] costs = new int[string2.Length];

            for (int i = 0; i < costs.Length;)
            {
                costs[i] = ++i;
            }

            for (int i = 0; i < string1.Length; i++)
            {
                int cost = i;
                int previousCost = i;

                char string1Char = string1[i];

                for (int j = 0; j < string2.Length; j++)
                {
                    int currCost = cost;
                    cost = costs[j];
                    if (string1Char != string2[j])
                    {
                        if (previousCost < currCost)
                        {
                            currCost = previousCost;
                        }
                        if (cost < currCost)
                        {
                            currCost = cost;
                        }
                        ++currCost;
                    }

                    costs[j] = currCost;
                    previousCost = currCost;
                }
            }
            return costs[costs.Length - 1];
        }
    }
}
