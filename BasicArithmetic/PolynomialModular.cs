using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BasicArithmetic
{
    class PolynomialModular : ExtendedModular
    {
        private Modular[] Coefficients { get; set; }

        #region constructors and initializers
        public PolynomialModular(PolynomialFieldRepresentation field, int length = 0)
            : base(field) 
        {
            InitializeToZero(length != 0 ? length : field.Dimension + 1);
        }

        public PolynomialModular(PolynomialFieldRepresentation field, int power, int length = 0)
            : base(field, power)
        {
            var generator = ((PolynomialFieldRepresentation)Field).Generator;

            if (generator == null)
                throw new GeneratorNotSetException("You must set generator to this field before generating elements.");

            InitializeToZero(power + 1);
            Coefficients[power] = new Modular(1, Field.Characteristic);

            var rest = this % generator;
            InitializeToZero(field.Dimension);
            if (rest.Coefficients.Length < field.Dimension)
                for (int i = 0; i < rest.Coefficients.Length; i++)
                    Coefficients[i] = rest[i];
            else
                for (int i = 0; i < field.Dimension; i++)
                    Coefficients[i] = rest[i];
        }

        public PolynomialModular(PolynomialFieldRepresentation field, BigInteger[] coefficients)
            :base(field)
        {
            Coefficients = new Modular[coefficients.Length];

            for (int i = 0; i < coefficients.Length; i++)
                Coefficients[i] = new Modular(coefficients[i], Field.Characteristic);
        }

        public PolynomialModular(PolynomialModular polynomial)
            : base(polynomial.Field)
        {
            Coefficients = new Modular[polynomial.Coefficients.Length];
            for (int i = 0; i < polynomial.Coefficients.Length; i++)
                Coefficients[i] = polynomial.Coefficients[i];
        }

        private void InitializeToZero(int length)
        {
            Coefficients = new Modular[length];
            for (int i = 0; i < length; i++)
                Coefficients[i] = new Modular(0, Field.Characteristic);
        }

        private static bool CheckCharacteristicAndExtension(PolynomialModular a, PolynomialModular b)
        {
            return (a.Field.Characteristic == b.Field.Characteristic) && (a.Field.Dimension == b.Field.Dimension);
        }
        #endregion

        #region operators
        public static PolynomialModular operator +(PolynomialModular a, PolynomialModular b)
        {
            if (!PolynomialModular.CheckCharacteristicAndExtension(a, b))
                throw new DifferentFieldsException("Both polynomials have to be over same field.");

            PolynomialModular result = new PolynomialModular((PolynomialFieldRepresentation)a.Field, length: Math.Max(a.Coefficients.Length, b.Coefficients.Length));
            for (int i = 0; i < a.Field.Dimension; i++)
                result[i] = a[i] + b[i];

            return result;
        }

        public static PolynomialModular operator -(PolynomialModular a, PolynomialModular b)
        {
            if (!PolynomialModular.CheckCharacteristicAndExtension(a, b))
                throw new DifferentFieldsException("Both polynomials have to be over same field.");
            int i;
            PolynomialModular result = new PolynomialModular((PolynomialFieldRepresentation)a.Field, length: Math.Max(a.Coefficients.Length, b.Coefficients.Length));
            for (i = 0; i < Math.Min(a.Coefficients.Length, b.Coefficients.Length); i++)
            {
                if (a[i] < 0)
                    a[i] = a[i].AdditiveInversion();
                if (b[i] < 0)
                    b[i] = b[i].AdditiveInversion();

                result[i] = a[i] - b[i];

                if (result[i] < 0)
                    result[i] = result[i].AdditiveInversion();
            }

            if(a.Coefficients.Length != b.Coefficients.Length)
            {
                for (; i < Math.Max(a.Coefficients.Length, b.Coefficients.Length); i++)
                {
                    if(a.Coefficients.Length < b.Coefficients.Length)
                        result[i] = b[i].AdditiveInversion();
                    else
                        result[i] = a[i];
                }
            }

            return result;
        }

        public static PolynomialModular operator *(PolynomialModular a, PolynomialModular b)
        {
            if (!PolynomialModular.CheckCharacteristicAndExtension(a, b))
                throw new DifferentFieldsException("Both polynomials have to be over same field.");

            PolynomialModular product = new PolynomialModular((PolynomialFieldRepresentation)a.Field, length: 2 * a.Field.Dimension + 1);
            
            for (int i = 0; i < a.Field.Dimension; i++)
                for (int j = 0; j < a.Field.Dimension; j++)
                    product[i + j] += (a[i] * b[j]);

            var result = product % ((PolynomialFieldRepresentation)a.Field).Generator;
            result.Trim();

            return result;
        }

        public static PolynomialModular operator %(PolynomialModular a, PolynomialModular b)
        {
            int degree = Degree(a);
            int divisorDegree = Degree(b);
            if (degree < divisorDegree)
                return new PolynomialModular(a);

            PolynomialModular result = new PolynomialModular((PolynomialFieldRepresentation)a.Field, length: degree - divisorDegree + 1);
            PolynomialModular rest = new PolynomialModular(a);

            while (Degree(rest) >= divisorDegree)
            {
                PolynomialModular temp = new PolynomialModular((PolynomialFieldRepresentation)a.Field, length: degree + 1);
                int elementDegree = degree - divisorDegree;
                result[elementDegree] = rest[degree] * b[divisorDegree].MultiplicativeInversion();

                for (int i = 0; i < b.Coefficients.Length; i++)
                    temp[elementDegree + i] = result[elementDegree] * b[i];

                rest = rest - temp;
                degree = Degree(rest);
            }

            return rest;
        }

        public static bool operator ==(PolynomialModular a, PolynomialModular b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null))
                return false;
            if (ReferenceEquals(b, null))
                return false;

            if (a.Field != b.Field)
                return false;

            if (a.Coefficients.Length != b.Coefficients.Length)
                return false;

            for (int i = 0; i < a.Coefficients.Length; i++)
                if (a[i] != b[i])
                    return false;

            return true;
        }

        public static bool operator !=(PolynomialModular a, PolynomialModular b)
        {
            if (ReferenceEquals(a, b))
                return false;
            if ((ReferenceEquals(a, null) || ReferenceEquals(b, null)) && (!ReferenceEquals(a, null) || !ReferenceEquals(b, null)))
                return true;

            if (a.Field != b.Field)
                return true;

            if (a.Coefficients.Length != b.Coefficients.Length)
                return true;

            for (int i = 0; i < a.Coefficients.Length; i++)
                if (a[i] != b[i])
                    return true;

            return false;
        }
        #endregion

        private void Trim()
        {
            var coefficients = Coefficients;
            Coefficients = new Modular[Field.Dimension];
            for (int i = 0; i < Field.Dimension; i++)
                Coefficients[i] = coefficients[i];
        }

        private static int Degree(PolynomialModular polynomial)
        {
            for (int i = polynomial.Coefficients.Count() - 1; i >= 0; i--)
                if (polynomial[i] != 0)
                    return i;

            return -1;
        }

        public Modular this[int key]
        {
            get
            {
                return Coefficients[key];
            }
            set
            {
                Coefficients[key] = value;
            }
        }

        public static List<PolynomialModular> FindIrreduciblePolynomials(PolynomialFieldRepresentation field)
        {
            List<PolynomialModular> result = new List<PolynomialModular>();
            var one = new PolynomialModular(field, power: 1);

            var minimalPolynomials = PolynomialModular.FindMinimalPolynomials(field.Characteristic, field.Dimension);

            foreach (var minimalPolynomial in minimalPolynomials)
            {
                List<PolynomialModular> elements = new List<PolynomialModular>();
                foreach (var element in minimalPolynomial)
                    elements.Add(new PolynomialModular(field, power: (int)element));

                PolynomialModular irreduciblePolynomial = elements[0] - one;
                for (int i = 1; i < elements.Count; i++)
                {
                    var par = elements[i] - one;
                    irreduciblePolynomial *= par;
                }

                result.Add(irreduciblePolynomial);
            }

            return result;
        }

        public static List<List<BigInteger>> FindMinimalPolynomials(BigInteger characteristic, int extension)
        {
            List<List<BigInteger>> result = new List<List<BigInteger>>();
            List<BigInteger> elements = new List<BigInteger>();
            BigInteger i = 1;

            while (elements.Count < Math.Pow((double)characteristic, extension) - 2)
            {
                if(elements.Contains(i))
                {
                    i++;
                    continue;
                }
                var minimalPolynomial = PolynomialModular.FindMinimalPolynomial(characteristic, extension, i);
                elements.AddRange(minimalPolynomial);
                result.Add(minimalPolynomial);
            }

            return result;
        }

        private static List<BigInteger> FindMinimalPolynomial(BigInteger characteristic, int extension, BigInteger index)
        {
            List<BigInteger> result = new List<BigInteger>();
            BigInteger i = index;

            do
            {
                result.Add(i);
                i = (i * 2) % (int)(Math.Pow((int)characteristic, extension) - 1);
            } while (index != i);

            return result;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Coefficients.Length; i++)
                result += ((i == 0) ? "[" : "") + Coefficients[i].Value + ((i == Coefficients.Length - 1) ? "]" : ", ");

            return result;
        }
    }
}