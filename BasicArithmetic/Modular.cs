using System;
using System.Collections.Generic;
using System.Numerics;

namespace BasicArithmetic
{
    class Modular
    {
        private BigInteger modulus;
        private BigInteger value;

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

            return new Modular((a.value + b.AdditiveInversion().value) % val1.modulus, val1.modulus);
        }

        public static Modular operator -(Modular val1, int val2)
        {
            var a = val1.value < 0 ? val1.AdditiveInversion() : val1;

            return new Modular((a.value + val2) % val1.modulus, val1.modulus);
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
        #endregion

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

        public static int GetNumberOfPrimitives()
        {
            // Euler's function
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public string ModularToString()
        {
            return String.Format("{0} (mod{1})", value, modulus);
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

        private Modular AdditiveInversion()
        {
            return new Modular(modulus - BigInteger.Abs(value), modulus);
        }

        private Modular MultiplicativeInversion()
        {
            var eulersFunction = new Modular(Modular.EulersFunction(modulus), modulus);
            var result = this ^ (eulersFunction - 1);

            return result;
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
    }
}