using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public interface ITool : IDisposable
    {
        #region VARIABLE
        IRecipe ORecipe { get; set; }
        #endregion


        #region FUNCTION
        void BeginSync();
        void EndSync();
        #endregion
    }
}
