using PU1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public interface IImageTool : ITool
    {
        IImageExporter OExporter { get; set; }
    }
}
