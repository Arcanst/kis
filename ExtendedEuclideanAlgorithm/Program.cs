using System;

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

            Console.ReadKey();
        }
    }
}