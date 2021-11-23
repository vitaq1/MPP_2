using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    class StringGenerator : IDTOGenerator
    {
        public Type generatedType { get; private set; }
        public Random random { get; private set; }
        public StringGenerator(Random rand)
        {
            random = rand;
            generatedType = typeof(string);
        }
        public object Generate()
        {
            const int MinLength = 3;
            const int MaxLength = 20;
            int length = random.Next(MinLength, MaxLength);
            const string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            int index;
            string substr, str = "";

            for (int i = 0; i < length; i++)
            {
                index = random.Next(symbols.Length);
                substr = symbols.Substring(index, 1);
                str += substr;
            }

            return str;
        }
    }
}
