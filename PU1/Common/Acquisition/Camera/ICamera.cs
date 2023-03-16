using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public interface ICamera : IImageExporter
    {
        CCameraInfo OCameraInfo { get; }

        int I32Width { get; set; }
        int I32WidthMin { get; }
        int I32WidthMax { get; }
        int I32Height { get; set; }
        int I32HeightMin { get; }
        int I32HeightMax { get; }
        bool BCenterX { get; set; }
        int I32Gain { get; set; }
        int I32GainMin { get; }
        int I32GainMax { get; }
        int I32ExposureTime { get; set; }
        int I32ExposureTimeMin { get; }
        int I32ExposureTimeMax { get; }
        string StrTriggerSelector { get; set; }
        string StrTriggerMode { get; set; }
        string StrTriggerSource { get; set; }
    }
}
