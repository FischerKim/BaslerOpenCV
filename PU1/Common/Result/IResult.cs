using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1.Common.Result
{
    public interface IResult
    {
        #region PROPERTIES
        bool BInspected { get; }
        bool BOK { get; }
        #endregion
    }
}
