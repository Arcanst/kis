using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BasicArithmetic
{
    class FiniteField
    {
        public BigInteger Characteristic { get; set; }
    }

    class ExtendedFiniteField : FiniteField
    {
        public int Dimension { get; set; }
    }

    class PolynomialFieldRepresentation : ExtendedFiniteField
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