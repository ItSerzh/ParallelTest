using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTest
{
    internal class ArrayGenerator
    {
        public int[] GetArray(int size)
        {
            var array = new int[size];
            var rnd = new Random();
            for (int i = 0; i< size; i++)
            {
                array[i] = rnd.Next(int.MinValue, 1_000_000);
            }
            return array;
        }
    }
}
