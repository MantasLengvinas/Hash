using System;
using System.Collections.Generic;
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

        public bool InputLenghtTest()
        {
            try
            {
                for (int i = 1; i < 10000; i *= 10)
                {
                    string output = hashService.Hash(data.GenerateRandomString(i), ref timeElapsed);
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }

        public bool OutputLenghtTest()
        {
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

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public int CollisionTest()
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

            return collisions;
        }
    }
}
