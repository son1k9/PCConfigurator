using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Model.Components.M2
{
    [Flags]
    public enum M2Size
    {
        _2230  = 0b00001,
        _2242  = 0b00010, 
        _2260  = 0b00100, 
        _2280  = 0b01000, 
        _22110 = 0b10000
    }

    [Flags]
    public enum M2Interface
    {
        Nvme = 0b01,
        Sata = 0b10
    }
}
