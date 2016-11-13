using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicArithmetic
{
    class DifferentFieldsException : Exception
    {
        public DifferentFieldsException()
        {
        }

        public DifferentFieldsException(string message)
            : base(message)
        {
        }
    }

    public class GeneratorNotSetException : Exception
    {
        public GeneratorNotSetException()
        {
        }

        public GeneratorNotSetException(string message)
            : base(message)
        {
        }
    }
}