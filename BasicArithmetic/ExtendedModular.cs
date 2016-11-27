using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BasicArithmetic
{
    public class ExtendedModular
    {
        public ExtendedFiniteField Field { get; set; }
        public int Power { get; set; }

        public ExtendedModular(ExtendedFiniteField field, int power = 0)
        {
            Field = field;
            Power = power;
        }
    }
}