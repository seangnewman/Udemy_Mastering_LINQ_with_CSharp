using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    public class Person
    {
        private string firstName, lastName, middleName;

        public Person(string firstname, string lastname, string middlename)
        {
            this.firstName = firstname;
            this.middleName = middlename;
            this.lastName = lastname;
        }

        public IEnumerable<string> Names
        {
            get
            {
                yield return firstName;
                yield return middleName;
                yield return lastName;
            }
             

        }
    }
}
