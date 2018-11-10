using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection_And_Filtering
{
    class Exercise
    {
        public static IEnumerable<int> MyFilter(IEnumerable<int> input)
        {
            return  input.Where(i => i % 2 == 0)
                .Select(r => r * r)
                .Where(r => r <= 50);

        }

        public static IEnumerable<int> Merge(IEnumerable<int> a, IEnumerable<int> b)
        {
            return a.Where(v => b.All(i => i != v))
                    .Union(b.Where(v => a.All(i => i != v))); ;
        }

        public static int LengthOfPositive(IEnumerable<int> input)
        {
            var temp = input.SkipWhile(i => i < 0)
                            .TakeWhile(r => r > 0);

            return temp.Count();
        }

        public static int Poly(int x, IEnumerable<int> coeffs)
        {
            var tempArray = coeffs.ToArray();
            
            if( tempArray.Length != 3)
            {
                return 0;
            }

            var a = tempArray[0];
            var b = tempArray[1];
            var c = tempArray[2];
            var degree = tempArray.Length;

            return (int)( coeffs.Take(1).Aggregate(0.0, (accum, v) => a * (int)Math.Pow(x, degree - 1)) + coeffs.Take(1).Aggregate(0.0, (accum, v) => b * Math.Pow(x, degree - 2)) + coeffs.Take(1).Aggregate(0.0, (accum, v) => c * Math.Pow(x, degree - 3)));
 
        }
    }
}
