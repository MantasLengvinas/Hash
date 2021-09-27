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
            string inputParam;

            HashService hashService = new HashService();

            inputParam = arguments.First();
            arguments.Remove(inputParam);

            if(inputParam == "-in")
            {
                List<Result> results = new List<Result>();

                foreach(string arg in arguments)
                {
                    results.Add(new Result() { Input = arg, Output = hashService.Hash(arg) });
                }


                foreach(Result r in results)
                {
                    Console.WriteLine("Input: {0} Output: {1} \n", r.Input, r.Output);
                }
            }
        }
    }
}
