using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    class Program
    {
        class A
        {
            public int integerNumber;
            public string str;
            public bool boolean;
            public float floatNumber;
            public override string ToString()
            {
                return $"int integerNumber = {integerNumber}, " +
                    $"\nstring str = {str}, " +
                    $"\nbool boolean = {boolean}, " +
                    $"\nfloat floatNumber = {floatNumber}";
            }
        }

        public static void Main(string[] args)
        {
            Faker faker = new Faker();
            A obj = faker.Create<A>();
            Console.WriteLine(obj);
            Console.ReadLine();
        }
    }
}
