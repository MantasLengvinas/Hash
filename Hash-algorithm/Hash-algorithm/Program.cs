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
            string inputParam = "", outputParam = "", settingsParam = "";
            List<Result> results = new List<Result>();
            TimeSpan timeElapsed = new TimeSpan();

            HashService hashService = new HashService();
            TestsService testsService = new TestsService();

            if (!arguments.First().Contains("-in"))
            {
                Console.WriteLine("You must to specify input parameter!");
                Environment.Exit(1);
            }
            else
            {
                inputParam = arguments.First();
                arguments.Remove(inputParam);
            }

            if (!arguments.First().Contains("-out"))
            {
                Console.WriteLine("You must to specify output parameter!");
                Environment.Exit(1);
            }
            else
            {
                outputParam = arguments.First();
                arguments.Remove(outputParam);
            }

            if (arguments.First().Contains("-"))
            {
                settingsParam = arguments.First();
                arguments.Remove(settingsParam);
            }
            
            if(settingsParam == "-t")
            {
                Console.WriteLine("Running tests..");
                testsService.RunTests();

            }
            else
            {
                if (inputParam == "-in")
                {
                    foreach (string arg in arguments)
                    {
                        results.Add(new Result() { Input = arg, Output = hashService.Hash(arg, ref timeElapsed) });
                    }
                }

                if (inputParam == "-inf")
                {
                    foreach (string arg in arguments)
                    {
                        string readedData = File.ReadAllText(arg);
                        if (readedData.Length > 0)
                        {
                            results.Add(new Result() { Input = readedData, Output = hashService.Hash(readedData, ref timeElapsed) });
                        }

                    }
                }


                if (outputParam == "-out")
                {
                    if (results.Count > 0)
                    {
                        foreach (Result r in results)
                        {
                            Console.WriteLine("Input: {0} Output: {1}", r.Input, r.Output);
                        }
                        Console.WriteLine("Hashing took {0}ms", timeElapsed);
                    }
                    else
                    {
                        Console.WriteLine("No results");
                    }
                }

                if (outputParam == "-outf")
                {
                    if (!Directory.Exists(AppContext.BaseDirectory + @"Results\"))
                    {
                        Directory.CreateDirectory(AppContext.BaseDirectory + @"Results\");
                    }

                    if (File.Exists(AppContext.BaseDirectory + @"Results\Results.txt"))
                    {
                        File.Delete(AppContext.BaseDirectory + @"Results\Results.txt");
                    }

                    foreach (Result r in results)
                    {
                        File.AppendAllText(AppContext.BaseDirectory + @"Results\Results.txt", String.Format("Input: {0} Output: {1}\n", r.Input, r.Output));
                    }

                    Console.WriteLine("Results have been saved to files");
                    Console.WriteLine("Hashing took {0}ms", timeElapsed.TotalMilliseconds);
                }
            }
        }
    }
}
