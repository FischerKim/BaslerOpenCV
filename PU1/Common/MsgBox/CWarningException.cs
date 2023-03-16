using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CWarningException : Exception
    {
        public CWarningException(string StrMessage) : base(StrMessage) { }
    }
}
