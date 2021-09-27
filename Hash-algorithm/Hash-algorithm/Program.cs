using Hash_algorithm.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hash_algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arguments = args.ToList();

            HashService hashService = new HashService();

            string hash = hashService.Hash("Mantas");
        }
    }
}
