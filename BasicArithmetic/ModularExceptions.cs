using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicArithmetic
{
    class DifferentModulusException : Exception
    {
        public DifferentModulusException()
        {
        }

        public DifferentModulusException(string message)
            : base(message)
        {
        }

        public DifferentModulusException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}