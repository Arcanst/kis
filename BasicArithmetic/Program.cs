using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Combinatorics;
using Combinatorics.Collections;

namespace BasicArithmetic
{
    class Program
    {
        static void Main(string[] args)
        {
            showIrreducibles();
        }

        public static void showIrreducibles()
        {
            PolynomialFieldRepresentation field1 = new PolynomialFieldRepresentation(2, 5);
            var irreducibles1 = field1.FindIrreduciblePolynomials();
            Console.WriteLine("Wielomiany nierozkładalne stopnia {0} nad ciałem GF({1}):", field1.Dimension, field1.Characteristic);
            foreach (var irreducible in irreducibles1)
                Console.WriteLine("\t{0}", irreducible);

            Console.ReadKey();
        }

        public static void showSomeMinimals()
        {
            var polynomials2 = Polynomial.FindMinimalPolynomials(2, 2);
            var polynomials3 = Polynomial.FindMinimalPolynomials(2, 3);
            var polynomials4 = Polynomial.FindMinimalPolynomials(2, 4);
            var polynomials5 = Polynomial.FindMinimalPolynomials(2, 5);

            Console.WriteLine("Ciało GF({0}^{1}):", 2, 2);
            int i = 0;
            foreach (var polynomial in polynomials2)
            {
                Console.WriteLine("Warstwa cyklotomiczna numer {0}:", polynomial[0]);
                for (int j = 0; j < polynomial.Count; j++)
                    Console.Write("{0}" + ((j == (polynomial.Count - 1)) ? "\n" : ", "), polynomial[j]);
                i++;
            }

            Console.WriteLine();
            Console.WriteLine("Ciało GF({0}^{1}):", 2, 3);
            i = 0;
            foreach (var polynomial in polynomials3)
            {
                Console.WriteLine("Warstwa cyklotomiczna numer {0}:", polynomial[0]);
                for (int j = 0; j < polynomial.Count; j++)
                    Console.Write("{0}" + ((j == (polynomial.Count - 1)) ? "\n" : ", "), polynomial[j]);
                i++;
            }

            Console.WriteLine();
            Console.WriteLine("Ciało GF({0}^{1}):", 2, 4);
            i = 0;
            foreach (var polynomial in polynomials4)
            {
                Console.WriteLine("Warstwa cyklotomiczna numer {0}:", polynomial[0]);
                for (int j = 0; j < polynomial.Count; j++)
                    Console.Write("{0}" + ((j == (polynomial.Count - 1)) ? "\n" : ", "), polynomial[j]);
                i++;
            }

            Console.WriteLine();
            Console.WriteLine("Ciało GF({0}^{1}):", 2, 5);
            i = 0;
            foreach (var polynomial in polynomials5)
            {
                Console.WriteLine("Warstwa cyklotomiczna numer {0}:", polynomial[0]);
                for (int j = 0; j < polynomial.Count; j++)
                    Console.Write("{0}" + ((j == (polynomial.Count - 1)) ? "\n" : ", "), polynomial[j]);
                i++;
            }

            Console.ReadKey();
        }

        public static void showAllTests()
        {
            Console.WriteLine("Największa dostępna liczba typu decimal: {0}", decimal.MaxValue);

            // http://ptrow.com/perl/calculator.pl check for correct answers
            Modular a = new Modular(5, 7);
            Modular b = new Modular(2, 7);
            Modular c = new Modular(4, 7);
            Modular d = new Modular(-5, 7);
            Modular e = new Modular(-3, 7);
            Modular f = new Modular(2, 2, false);

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

            var primitives1 = Modular.GetPrimitives(number2);

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
                Console.WriteLine("\tdla n={0}: {1}, jest pierwsza: {2}", i, eulersFunction, eulersFunction == i - 1 ? "TAK" : "NIE");
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
            PolynomialFieldRepresentation field3 = new PolynomialFieldRepresentation(2, 3, new BigInteger[] { 1, 0, 0, 1 });

            Polynomial polynomial1 = new Polynomial(field1, new BigInteger[] { 0, 0, 0, 0, 0, 0, 1 });
            Polynomial polynomial2 = new Polynomial(field1, new BigInteger[] { 1, 1, 0, 1 });
            Polynomial polynomial3 = new Polynomial(field1, new BigInteger[] { 1, 1, 0, 1, 1, 0, 0, 0, 0, 1 });
            Polynomial polynomial4 = new Polynomial(field1, new BigInteger[] { 1, 1, 1 });
            Polynomial polynomial5 = new Polynomial(field2, new BigInteger[] { 1, 0, 1, 2 }); //3
            Polynomial polynomial6 = new Polynomial(field2, new BigInteger[] { 1, 2, 2 });    //3
            Polynomial polynomial7 = new Polynomial(field3, new BigInteger[] { 1, 0, 1 });
            Polynomial polynomial8 = new Polynomial(field3, new BigInteger[] { 0, 1, 0 });

            var minimalPolynomials1 = Polynomial.FindMinimalPolynomials(2, 4);

            //var irreduciblePolynomials1 = PolynomialModular.FindIrreduciblePolynomials(field1);

            var division1 = polynomial1 % polynomial2;
            var division2 = polynomial3 % polynomial4;
            var division3 = polynomial5 % polynomial6;
            var division4 = polynomial7 % polynomial8;

            Console.WriteLine("{0} % {1} = {2}", polynomial1, polynomial2, division1);
            Console.WriteLine("{0} % {1} = {2}", polynomial3, polynomial4, division2);
            Console.WriteLine("{0} % {1} = {2}", polynomial5, polynomial6, division3);
            Console.WriteLine("{0} % {1} = {2}", polynomial7, polynomial8, division4);


            for (int i = 0; i < minimalPolynomials1.Count; i++)
            {
                Console.WriteLine("Warstwa cyklotomiczna numer {0}:", minimalPolynomials1[i][0]);
                for (int j = 0; j < minimalPolynomials1[i].Count; j++)
                    Console.Write("{0}" + ((j == (minimalPolynomials1[i].Count - 1)) ? "\n" : ", "), minimalPolynomials1[i][j]);
            }

            Console.WriteLine("Elementy ciała {0}:", field1);
            for (int i = 0; i < (int)Math.Pow((int)field1.Characteristic, field1.Dimension) - 1; i++)
                Console.WriteLine("\ta^{0}:\t{1}", i, new Polynomial(field1, power: i));

            Console.WriteLine("Algorytm Euklidesa dla wielomianów {0} i {1}.", polynomial3, polynomial3);
            Console.WriteLine(Polynomial.Euclids(polynomial3, polynomial3));

            var polynomialValueForArgument1 = polynomial2.CalculateForArgument(f);

            Console.WriteLine("Wartość wielomianu {0} dla argumentu {1}:\t{2}", polynomial2, f, polynomialValueForArgument1);
            Console.WriteLine("Powyższy wielomian jest nierozkładalny:\t{0}", polynomialValueForArgument1.IsPrime() ? "NIE" : "TAK");

            var irreducibles1 = field1.FindIrreduciblePolynomials();
            var irreducibles2 = field3.FindIrreduciblePolynomials();
            var irreducibles3 = field2.FindIrreduciblePolynomials();

            Console.WriteLine("Wielomiany nierozkładalne stopnia {0} nad ciałem GF({1}):", field1.Dimension, field1.Characteristic);
            foreach (var irreducible in irreducibles1)
                Console.WriteLine("\t{0}", irreducible);

            Console.WriteLine("Wielomiany nierozkładalne stopnia {0} nad ciałem GF({1}):", field3.Dimension, field3.Characteristic);
            foreach (var irreducible in irreducibles2)
                Console.WriteLine("\t{0}", irreducible);

            Console.WriteLine("Wielomiany nierozkładalne stopnia {0} nad ciałem GF({1}):", field2.Dimension, field2.Characteristic);
            foreach (var irreducible in irreducibles3)
                Console.WriteLine("\t{0}", irreducible);

            Console.ReadKey();
        }
    }
}