using System;
using System.Numerics;

namespace KiS
{
    /// <summary>
    /// Extended Euclidean Algorithm
    /// </summary>
    class ExtendedEuclideanAlgorithm
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            BigInteger a = 36, b = 12;

            Console.WriteLine(shortEuclids(a, b));
            Console.ReadKey();
        }

        public static BigInteger shortEuclids(BigInteger a, BigInteger b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a > b)
                return shortEuclids(a % b, b);
            else
                return shortEuclids(a, b % a);
        }

        public static void longEuclids()
        {
            int r, a, q, b;
            int x, x1, x2;
            int y, y1, y2;
            int nwd_a, nwd_b, nwd;

            //get all data
            Console.WriteLine("Podaj pierwsza liczbe");
            nwd_a = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj druga liczbe");
            nwd_b = int.Parse(Console.ReadLine());

            // a must be greater than b
            if (nwd_b > nwd_a)
            {
                nwd = nwd_b;
                nwd_b = nwd_a;
                nwd_a = nwd;
            }

            //initialize a and b
            a = nwd_a;
            b = nwd_b;

            //initialize r and nwd
            q = a / b;
            r = a - q * b;
            nwd = b;

            //initialize x and y
            x2 = 1;
            x1 = 0;
            y2 = 0;
            y1 = 1;
            x = 1;
            y = y2 - (q - 1) * y1;

            while (r != 0)
            {
                a = b;
                b = r;

                x = x2 - q * x1;
                x2 = x1;
                x1 = x;

                y = y2 - q * y1;
                y2 = y1;
                y1 = y;

                nwd = r;
                q = a / b;
                r = a - q * b;
            }

            Console.WriteLine("NWD(" + nwd_a + ", " + nwd_b + ") = " + nwd + " = " + x + " * " + nwd_a + " + " + y + " * " + nwd_b);

            if (nwd == 1)
                Console.WriteLine(nwd_b + " * " + y + " mod " + nwd_a + " = 1");
        }
    }
}