using System;
using System.Numerics;

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

            BigInteger number1 = 100;
            BigInteger number2 = 11;

            var divisors1 = Modular.FindAllDivisors(number1);
            var primitives1 = Modular.GetPrimitives(number2);
            var primeFactors1 = Modular.FindAllPrimeFactors(number1);

            Console.WriteLine("{0} + {1} = {2}\t{3}", a, b, res1, 0);
            Console.WriteLine("{0} + {1} = {2}\t{3}", c, a, res2, 2);
            Console.WriteLine("{0} - {1} = {2}\t{3}", c, b, res3, 2);
            Console.WriteLine("{0} - {1} = {2}\t{3}", c, d, res4, 2);
            Console.WriteLine("{0} + {1} = {2}\t{3}", c, d, res5, 6);
            Console.WriteLine("{0} * {1} = {2}\t{3}", c, b, res6, 1);
            Console.WriteLine("{0} * {1} = {2}\t{3}", c, d, res7, 1);
            Console.WriteLine("{0} * {1} = {2}\t{3}", a, d, res8, 3);
            Console.WriteLine("{0} * {1} = {2}\t{3}", d, e, res9, 1);
            Console.WriteLine("{0} ^ {1} = {2}\t{3}", a, b, res10, 4);
            Console.WriteLine("{0} ^ {1} = {2}\t{3}", d, c, res11, 2);
            Console.WriteLine("{0} ^ {1} = {2}\t{3}", b, e, res12, 2);

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

            Console.WriteLine("Elementy prymitywne w ciele GF({0}):", number2);
            for (int i = 0; i < primitives1.Count; i++)
                Console.Write("{0}" + (i == (primitives1.Count - 1) ? "\n" : ", "), primitives1[i]);

            for (int i = 1; i < modulus; i++)
            {
                Modular element = new Modular(i, modulus);
                Console.WriteLine("Odwrotność multiplikatywna liczby {0} mod{1} = {2}", element, element.Modulus, element.MultiplicativeInversion());
            }

            Console.WriteLine("Wynik Algorytmu Euklidesa dla {0} i {1}", number1, number2);
            Console.WriteLine(Modular.Euclids(number1, number2));

            Console.WriteLine("------------------ POLYNOMIALS ------------------");
            PolynomialFieldRepresentation field1 = new PolynomialFieldRepresentation(2, 4, new BigInteger[] { 1, 1, 0, 0, 1 });
            PolynomialFieldRepresentation field2 = new PolynomialFieldRepresentation(3, 2);

            PolynomialModular polynomial1 = new PolynomialModular(field1, new BigInteger[] { 0, 0, 0, 0, 0, 0, 1 });
            PolynomialModular polynomial2 = new PolynomialModular(field1, new BigInteger[] { 1, 1, 0, 1 });
            PolynomialModular polynomial3 = new PolynomialModular(field1, new BigInteger[] { 1, 1, 0, 1, 1, 0, 0, 0, 0, 1 });
            PolynomialModular polynomial4 = new PolynomialModular(field1, new BigInteger[] { 1, 1, 1 });
            PolynomialModular polynomial5 = new PolynomialModular(field2, new BigInteger[] { 1, 0, 1, 2 }); //3
            PolynomialModular polynomial6 = new PolynomialModular(field2, new BigInteger[] { 1, 2, 2 });    //3

            var irreducibles1 = PolynomialModular.FindMinimalPolynomials(2, 4);

            var irreduciblePolynomials1 = PolynomialModular.FindIrreduciblePolynomials(field1);

            var division1 = polynomial1 % polynomial2;
            var division2 = polynomial3 % polynomial4;
            var division3 = polynomial5 % polynomial6;

            int power1 = 6;

            Console.WriteLine("{0} % {1} = {2}", polynomial1, polynomial2, division1);
            Console.WriteLine("{0} % {1} = {2}", polynomial3, polynomial4, division2);
            Console.WriteLine("{0} % {1} = {2}", polynomial5, polynomial6, division3);

            
            for (int i = 0; i < irreducibles1.Count; i++)
            {
                Console.WriteLine("Warstwa cyklotomiczna numer {0}:", irreducibles1[i][0]);
                for (int j = 0; j < irreducibles1[i].Count; j++)
                    Console.Write("{0}" + ((j == (irreducibles1[i].Count - 1)) ? "\n" : ", "), irreducibles1[i][j]);
            }

            Console.WriteLine("Elementy ciała {0}:", field1);
            for (int i = 0; i < (int)Math.Pow((int)field1.Characteristic, field1.Dimension) - 1; i++)
                Console.WriteLine("\ta^{0}:\t{1}", i, new PolynomialModular(field1, power: i));

            Console.WriteLine("Wielomiany nierozkładalne nad {0}:", field1);
            for (int i = 0; i < irreduciblePolynomials1.Count; i++)
                Console.WriteLine("\t{0}", irreduciblePolynomials1[i]);

            //Console.WriteLine("Algorytm Euklidesa dla wielomianów.");
            //Console.WriteLine(PolynomialModular.Euclids(polynomial3, polynomial1));

            Console.ReadKey();
        }
    }
}