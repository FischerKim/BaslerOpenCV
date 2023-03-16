using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public interface IImageExporter
    {
        #region DELEGATE & EVENT
        event ExportedHandler Exported;
        #endregion


        #region PROPERTIES
        bool BRun { get; }
        #endregion


        #region FUNCTION
        void OneShot();
        void Start();
        void Stop();
        #endregion
    }

    public delegate void ExportedHandler(object OSource);
}
