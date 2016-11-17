using System;
using System.Collections.Generic;
using System.Numerics;

namespace BasicArithmetic
{
    public class Modular
    {
        private BigInteger modulus;
        private BigInteger value;

        #region constructors, getters, setters
        public BigInteger Value
        {
            get
            {
                return value;
            }
        }

        public BigInteger Modulus
        {
            get
            {
                return modulus;
            }
        }

        public Modular(BigInteger value, BigInteger modulus)
        {
            this.modulus = modulus;
            this.value = (int)value % this.modulus;
        }

        public Modular(Modular modular)
        {
            value = (int)modular.value;
            modulus = modular.modulus;
        }
        #endregion

        #region operators
        public static Modular operator +(Modular val1, Modular val2)
        {
            if (val1.modulus != val2.modulus)
                throw new DifferentModulusException("When adding, both numbers must share a common modulus value.");

            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            var b = val2.value < 0 ? val2.AdditiveInversion() : val2;

            return new Modular((a.value + b.value) % val1.modulus, val1.modulus);
        }

        public static Modular operator +(Modular val1, int val2)
        {
            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;

            return new Modular((a.value + val2) % val1.modulus, val1.modulus);
        }

        public static Modular operator -(Modular val1, Modular val2)
        {
            if (val1.modulus != val2.modulus)
                throw new DifferentModulusException("When subtracting, both numbers must share a common modulus value.");

            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            var b = val2.value < 0 ? val2.AdditiveInversion() : val2;
            var result = new Modular((a.value + b.AdditiveInversion().value) % val1.modulus, val1.modulus);
            if (result < 0)
                result = result.AdditiveInversion();
            return result;
        }

        public static Modular operator -(Modular val1, int val2)
        {
            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            var b = new Modular(val2, val1.Modulus);
            b = b < 0 ? b.AdditiveInversion() : b;
            var result = new Modular((a.value - val2) % val1.modulus, val1.modulus);
            if (result < 0)
                result = result.AdditiveInversion();
            return result;
        }

        public static Modular operator *(Modular val1, Modular val2)
        {
            if (val1.modulus != val2.modulus)
                throw new DifferentModulusException("When multiplying, both numbers must share a common modulus value.");

            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            var b = val2.value < 0 ? val2.AdditiveInversion() : val2;

            return new Modular((a.value * b.value) % val1.modulus, val1.modulus);
        }

        public static Modular operator ^(Modular val, Modular power)
        {
            if (val.modulus != power.modulus)
                throw new DifferentModulusException("When raising to a given power, both numbers must share a common modulus value.");

            var a = val.value < 0 ? val.AdditiveInversion() : val;
            var b = power.value < 0 ? power.AdditiveInversion() : power;

            Modular result = new Modular(val);

            for (int i = 0; i < power.value - 1; i++)
                result *= val;

            return result;
        }

        public static Modular operator ^(Modular val, BigInteger power)
        {
            var a = val.value < 0 ? val.AdditiveInversion() : val;

            Modular result = new Modular(val);

            for (int i = 0; i < power - 1; i++)
                result *= val;

            return result;
        }

        public static bool operator ==(Modular val1, Modular val2)
        {
            if (val1.modulus != val2.modulus)
                throw new DifferentModulusException("When checking equality, both numbers must share a common modulus value.");

            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            var b = val2.value < 0 ? val2.AdditiveInversion() : val2;

            return a.value == b.value;
        }

        public static bool operator !=(Modular val1, Modular val2)
        {
            if (val1.modulus != val2.modulus)
                throw new DifferentModulusException("When checking inequality, both numbers must share a common modulus value.");

            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            var b = val2.value < 0 ? val2.AdditiveInversion() : val2;

            return !(a.value == b.value);
        }

        public static bool operator ==(Modular val1, BigInteger val2)
        {
            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            val2 %= val1.modulus;

            return a.value == val2;
        }

        public static bool operator !=(Modular val1, BigInteger val2)
        {
            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;
            val2 %= val1.modulus;

            return !(a.value == val2);
        }

        public static bool operator <(Modular val1, Modular val2)
        {
            return val1.value < val2.value;
        }

        public static bool operator >(Modular val1, Modular val2)
        {
            return val1.value > val2.value;
        }

        public static bool operator <(Modular val1, int val2)
        {
            return val1.value < val2;
        }

        public static bool operator >(Modular val1, int val2)
        {
            return val1.value > val2;
        }

        public static Modular operator ++(Modular val1)
        {
            val1.value++;
            return val1;
        }

        public static Modular operator !(Modular val1)
        {
            var eulersFunction = new Modular(Modular.EulersFunction(val1.Modulus), val1.Modulus);
            var result = val1 ^ (eulersFunction - 1);

            return result;
        }
        #endregion

        public string GetCyclicGroupToString()
        {
            var cyclicGroup = GetCyclicGroup();

            return string.Join(",", cyclicGroup);
        }

        public bool IsPrimary()
        {
            return GetCyclicGroup().Count == modulus - 1;
        }

        public int GetOrder()
        {
            return GetCyclicGroup().Count;
        }

        public string ModularToString()
        {
            return String.Format("{0} (mod{1})", value, modulus);
        }

        private List<Modular> GetCyclicGroup()
        {
            List<Modular> result = new List<Modular>();
            Modular tmp = new Modular(this);
            result.Add(tmp);

            while (true)
            {
                tmp *= this;
                result.Add(tmp);
                if (tmp == 1)
                    break;
            }

            return result;
        }

        public Modular AdditiveInversion()
        {
            return new Modular(modulus - BigInteger.Abs(value), modulus);
        }

        public Modular MultiplicativeInversion()
        {
            var eulersFunction = new Modular(Modular.EulersFunction(modulus), modulus);
            var result = this ^ (eulersFunction - 1);

            return result;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        #region static
        public static string GetAdditiveGroupToString(BigInteger modulus)
        {
            var group = Modular.GetAdditiveGroup(modulus);
            return Modular.GroupToString(group, modulus);
        }

        public static string GetMultiplicativeGroupToString(BigInteger modulus)
        {
            var group = Modular.GetMultiplicativeGroup(modulus);
            return Modular.GroupToString(group, modulus);
        }

        public static BigInteger GetNumberOfPrimitives(BigInteger modulus)
        {
            // Euler's function
            return Modular.EulersFunction(modulus);
        }

        public static List<Modular> GetPrimitives(BigInteger modulus)
        {
            List<Modular> primitives = new List<Modular>();

            for (BigInteger i = 2; i < modulus - 1; i++)
            {
                Modular element = new Modular(i, modulus);
                if (element.GetCyclicGroup().Count == modulus - 1)
                    primitives.Add(element);
            }

            return primitives;
        }

        public static BigInteger EulersFunction(BigInteger modulus)
        {
            BigInteger result = modulus;   // Initialize result as n

            // Consider all prime factors of n and subtract their
            // multiples from result
            for (int p = 2; p * p <= modulus; ++p)
            {
                // Check if p is a prime factor.
                if (modulus % p == 0)
                {
                    // If yes, then update n and result 
                    while (modulus % p == 0)
                        modulus /= p;
                    result -= result / p;
                }
            }

            // If n has a prime factor greater than sqrt(n)
            // (There can be at-most one such prime factor)
            if (modulus > 1)
                result -= result / modulus;
            return result;
        }

        // TODO
        public static List<BigInteger> FindAllDivisors(BigInteger number)
        {
            List<BigInteger> divisors = new List<BigInteger>();

            for (BigInteger i = 2; i*i < number; i++)
            {
                if (number % i == 0)
                {
                    divisors.Add(i);

                    if (i != number / i)
                        divisors.Add(number / i);
                }
            }
            divisors.Sort();
            return divisors;
        }

        // TODO
        public static List<BigInteger> FindAllPrimeFactors(BigInteger modulus)
        {
            List<BigInteger> primeFactors = new List<BigInteger>();

            for (BigInteger i = 1; i * i <= modulus; i++)
            {
                if ((modulus % i) == 0)
                {
                    if (!primeFactors.Contains(i))
                        primeFactors.Add(i);
                }
            }

            primeFactors.Sort();
            return primeFactors;
        }

        private static BigInteger[,] GetAdditiveGroup(BigInteger modulus)
        {
            var result = new BigInteger[(int)modulus, (int)modulus];
            for (int i = 0; i < modulus; i++)
            {
                for (int j = 0; j < modulus; j++)
                {
                    var a = new Modular(i, modulus);
                    var b = new Modular(j, modulus);
                    result[i, j] = (BigInteger)(a + b).value;
                }
            }

            return result;
        }

        private static BigInteger[,] GetMultiplicativeGroup(BigInteger modulus)
        {
            BigInteger[,] result = new BigInteger[(int)modulus, (int)modulus];
            for (int i = 0; i < modulus; i++)
            {
                for (int j = 0; j < modulus; j++)
                {
                    var a = new Modular(i, modulus);
                    var b = new Modular(j, modulus);
                    result[i, j] = (BigInteger)(a * b).value;
                }
            }

            return result;
        }

        private static string GroupToString(BigInteger[,] group, BigInteger modulus)
        {
            string result = string.Empty;

            for (int i = 0; i < modulus; i++)
            {
                for (int j = 0; j < modulus; j++)
                {
                    result += group[i, j];
                    if (j != (modulus - 1))
                        result += "\t";
                    else
                        result += "\n";
                }
            }

            return result;
        }
        #endregion static
    }
}