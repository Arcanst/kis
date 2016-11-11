using System;

namespace BasicArithmetic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Największa dostępna liczba typu decimal: {0}", decimal.MaxValue);

            // http://ptrow.com/perl/calculator.pl check for correct answers
            Modular a = new Modular(5, 7);
            Modular b = new Modular(2, 7);
            Modular c = new Modular(4, 7);
            Modular d = new Modular(-5, 7);
            Modular e = new Modular(-3, 7);
            int modulus = 11, maxModulus = 21;

            var res1 = a + b;
            var res2 = c + a;
            var res3 = c - b;
            var res4 = c - d;
            var res5 = c + d;
            var res6 = c * b;
            var res7 = c * d;
            var res8 = a * d;
            var res9 = d * e;
            var res10 = a ^ b;
            var res11 = d ^ c;
            var res12 = b ^ e;

            Console.WriteLine("{0} + {1} = {2}", a, b, res1);
            Console.WriteLine("{0} + {1} = {2}", c, a, res2);
            Console.WriteLine("{0} - {1} = {2}", c, b, res3);
            Console.WriteLine("{0} - {1} = {2}", c, d, res4);
            Console.WriteLine("{0} + {1} = {2}", c, d, res5);
            Console.WriteLine("{0} * {1} = {2}", c, b, res6);
            Console.WriteLine("{0} * {1} = {2}", c, d, res7);
            Console.WriteLine("{0} * {1} = {2}", a, d, res8);
            Console.WriteLine("{0} * {1} = {2}", d, e, res9);
            Console.WriteLine("{0} ^ {1} = {2}", a, b, res10);
            Console.WriteLine("{0} ^ {1} = {2}", d, c, res11);
            Console.WriteLine("{0} ^ {1} = {2}", b, e, res12);

            Console.WriteLine("\nTabliczka dodawania mod{0}:\n{1}", modulus, Modular.GetAdditiveGroupToString(modulus));
            Console.WriteLine("\nTabliczka mnożenia mod{0}:\n{1}", modulus, Modular.GetMultiplicativeGroupToString(modulus));

            Console.WriteLine("\nCiało GF({0}) - liczba elementów pierwotnych (generatorów): {1}", modulus, Modular.EulersFunction(modulus - 1));
            for (int i = 2; i < modulus; i++)
            {
                Modular number = new Modular(i, modulus);
                Console.WriteLine("Element {0} ciała GF({1})", number.Value, number.Modulus);
                Console.WriteLine("\tGrupa cykliczna elementu: {0}", number.GetCyclicGroupToString());
                Console.WriteLine("\tRząd elementu: {0}", number.GetOrder());
                Console.WriteLine("\tJest elementem pierwotnym: {0}", number.IsPrimary() ? "TAK" : "NIE");
            }

            Console.WriteLine("\nFunkcja Eulera");
            for (int i = 0; i < maxModulus; i++)
            {
                var eulersFunction = Modular.EulersFunction(i);
                Console.WriteLine("\tdla n={0}: {1}, jest pierwsza: {2}", i, eulersFunction, eulersFunction == i -1 ? "TAK" : "NIE");
            }

            Console.ReadKey();
        }
    }
}