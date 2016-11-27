using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BasicArithmetic
{
    public class FiniteField
    {
        public BigInteger Characteristic { get; set; }
    }

    public class ExtendedFiniteField : FiniteField
    {
        public int Dimension { get; set; }
    }

    public class PolynomialFieldRepresentation : ExtendedFiniteField
    {
        public Polynomial Generator { get; set; }

        public PolynomialFieldRepresentation(BigInteger characteristic, int dimension)
        {
            Characteristic = characteristic;
            Dimension = dimension;
        }

        public PolynomialFieldRepresentation(BigInteger characteristic, int dimension, BigInteger[] generator)
        {
            Characteristic = characteristic;
            Dimension = dimension;
            Generator = new Polynomial(this, generator);
        }

        public PolynomialFieldRepresentation(BigInteger characteristic, int dimension, Polynomial generator)
        {
            Characteristic = characteristic;
            Dimension = dimension;
            Generator = generator;
        }

        public List<Polynomial> FindIrreduciblePolynomials()
        {
            List<Polynomial> result = new List<Polynomial>();
            var elements = Modular.GetAllElements(this.Characteristic);
            var variations = new Variations<BigInteger>(elements, this.Dimension + 1, GenerateOption.WithRepetition);

            foreach (var variation in variations)
            {
                if (variation[this.Dimension] == 0)
                    continue;
                Polynomial polynomial = new Polynomial(this, variation.ToArray());
                if (polynomial.CalculateForArgument(new Modular(this.Characteristic, this.Characteristic, false)).IsPrime())
                    result.Add(polynomial);
            }

            return result;
        }

        public static bool operator ==(PolynomialFieldRepresentation field1, PolynomialFieldRepresentation field2)
        {
            return (field1.Characteristic == field2.Characteristic) && (field1.Generator == field2.Generator);
        }

        public static bool operator !=(PolynomialFieldRepresentation field1, PolynomialFieldRepresentation field2)
        {
            return (field1.Characteristic != field2.Characteristic) || (field1.Generator != field2.Generator);
        }

        public override string ToString()
        {
            return String.Format("GF({0}^{1})", Characteristic, Dimension);
        }
    }
}