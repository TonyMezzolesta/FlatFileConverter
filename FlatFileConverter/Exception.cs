using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileConverter
{
    class ConvertException : Exception
    {
        public ConvertException()
            : base() { }

        public ConvertException(string message)
            : base(message) { }

        public ConvertException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ConvertException(string message, Exception innerException)
            : base(message, innerException) { }

        public ConvertException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
