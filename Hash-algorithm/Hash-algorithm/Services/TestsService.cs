using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_algorithm.Services
{
    class TestsService
    {

        HashService hashService = new HashService();
        Data data = new Data();
        TimeSpan timeElapsed = new TimeSpan();

        public void RunTests()
        {
            OutputLenghtTest();
            Console.WriteLine("----------------------------------");
            CollisionTest();
            Console.WriteLine("----------------------------------");
            SimilarityTest();
            Console.WriteLine("----------------------------------");
            SpeedTest();
        }

        private void OutputLenghtTest()
        {
            Console.WriteLine("Running output lenght test..");

            try
            {
                for (int i = 1; i < 10000; i *= 10)
                {
                    string output = hashService.Hash(data.GenerateRandomString(i), ref timeElapsed);

                    if (output.Length == 64)
                        continue;
                    else
                        throw new Exception("Output is not 256 bits");
                }

                Console.WriteLine("All result length 256 bits");
            }
            catch (Exception)
            {
                Console.WriteLine("Output length test failed - output length not 256 bits");
            }

        }
        private void CollisionTest()
        {
            Console.WriteLine("Starting collision test..");

            int collisions = 0;
            for(int i = 0; i < 100000; i++)
            {
                string s1 = data.GenerateRandomString(6);
                string s2 = data.GenerateRandomString(6);

                if (hashService.Hash(s1, ref timeElapsed) == hashService.Hash(s2, ref timeElapsed))
                    collisions++;

            }

            if(collisions == 0)
            {
                Console.WriteLine("No collisions occured!");
            }
            else
            {
                Console.WriteLine("{0} collisions occured!", collisions);
            }
        }

        private void SimilarityTest()
        {
            Console.WriteLine("Starting similarity tests..");

            Console.WriteLine("Running HEX similarity test..");

            List<float> percentage = new List<float>();
            TimeSpan timeElapsed = new TimeSpan();

            float min = 100, max = 0, avg = 0;

            for (int i = 0; i < 100000; i++)
            {
                string s1 = data.GenerateRandomString(6);
                string s2 = data.GenerateRandomString(6);

                percentage.Add(SimilarityCalculation(hashService.Hash(s1, ref timeElapsed), hashService.Hash(s2, ref timeElapsed)));

            }

            foreach(float p in percentage)
            {
                if (p < min)
                    min = p;
                if (p > max)
                    max = p;

                avg += p;
            }

            Console.WriteLine("Min HEX difference: {0:F2}%", min);
            Console.WriteLine("Max HEX difference: {0:F2}%", max);
            Console.WriteLine("Avg HEX difference: {0:F2}%", avg);

            Console.WriteLine("Running binary similarity test");

            min = 100;
            max = 0;
            avg = 0;

            for (int i = 0; i < 100000; i++)
            {
                string s1 = data.GenerateRandomString(6);
                string s2 = data.GenerateRandomString(6);

                BitArray bitArray1 = new BitArray(Encoding.UTF8.GetBytes(hashService.Hash(s1, ref timeElapsed)));
                BitArray bitArray2 = new BitArray(Encoding.UTF8.GetBytes(hashService.Hash(s2, ref timeElapsed)));

                bitArray1.Xor(bitArray2);

                float bits = 0;
                foreach (bool bit in bitArray1)
                    if (bit) bits++;

                float diff = 100 * (bits / 512);

                if (diff < min)
                    min = diff;

                if (diff > max)
                    max = diff;

                avg += diff;

            }

            Console.WriteLine("Min binary difference: {0:F2}%", min);
            Console.WriteLine("Max binary difference: {0:F2}%", max);
            Console.WriteLine("Avg binary difference: {0:F2}%", avg);

        }


        private void SpeedTest()
        {
            Console.WriteLine("Running hash speed test..");
            Console.WriteLine("Reading file konstitucija.txt \n");

            List<string> lines = File.ReadAllLines("konstitucija.txt").ToList();
            List<Result> results = new List<Result>();

            timeElapsed = new TimeSpan();

            foreach(string l in lines)
            {
                results.Add(new Result() { Input = l, Output = hashService.Hash(l, ref timeElapsed) });
            }

            if (!Directory.Exists(AppContext.BaseDirectory + @"Results\"))
            {
                Directory.CreateDirectory(AppContext.BaseDirectory + @"Results\");
            }

            if (File.Exists(AppContext.BaseDirectory + @"Results\Results.txt"))
            {
                File.Delete(AppContext.BaseDirectory + @"Results\Results.txt");
            }

            File.AppendAllText(AppContext.BaseDirectory + @"Results\Results.txt", "Konstitucija.txt every line hashing..");

            foreach (Result r in results)
            {
                File.AppendAllText(AppContext.BaseDirectory + @"Results\Results.txt", String.Format("Input: {0} Output: {1}\n", r.Input, r.Output));
            }

            Console.WriteLine("Speed test results have been saved to files");
            Console.WriteLine("Hashing took {0}ms", timeElapsed.TotalMilliseconds);

        }

        private float SimilarityCalculation(string s1, string s2)
        {
            if ((s1 == null) || (s2 == null)) return 0;
            if ((s1.Length == 0) || (s2.Length == 0)) return 0;
            if (s1 == s2) return 100;
            int similarity = LevenshteinDistance(s1, s2);
            return 100 - ((float)Math.Max(s1.Length, s2.Length) - (float)similarity) / (float)Math.Max(s1.Length, s2.Length) * 100;
        }

        private int LevenshteinDistance(string source1, string source2)
        {
            var source1Length = source1.Length;
            var source2Length = source2.Length;

            var matrix = new int[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
                return source2Length;

            if (source2Length == 0)
                return source1Length;

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
            for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

            // Calculate rows and collumns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            // return result
            return matrix[source1Length, source2Length];
        }
    }
}
