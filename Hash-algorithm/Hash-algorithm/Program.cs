using Hash_algorithm.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hash_algorithm
{

    class Program
    {
        static void Main(string[] args)
        {
            List<string> arguments = args.ToList();
            string inputParam;
            List<Result> results = new List<Result>();

            HashService hashService = new HashService();

            inputParam = arguments.First();
            arguments.Remove(inputParam);

            if(inputParam == "-in")
            {
                foreach(string arg in arguments)
                {
                    results.Add(new Result() { Input = arg, Output = hashService.Hash(arg) });
                }
            }

            if(inputParam == "-inf")
            {
                foreach(string arg in arguments)
                {
                    string readedData = File.ReadAllText(arg);

                    results.Add(new Result() { Input = readedData, Output = hashService.Hash(readedData)});
                }
            }


            foreach (Result r in results)
            {
                Console.WriteLine("Input: {0} Output: {1}", r.Input, r.Output);
            }
        }
    }
}
