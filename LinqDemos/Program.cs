using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Params(1, 2, 3);

            foreach (var item in p)
            {
                Console.WriteLine(item);
            }

            var Person = new Person("Vladimir", "Ilyitich", "Lenin");

            foreach (var name in Person.Names)
            {
                Console.Write(name + " ");
            }
            Console.WriteLine("");

        }

        
    }
}
