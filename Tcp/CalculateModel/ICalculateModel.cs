using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    public interface ICalculateModel
    {
        //用于计算传感器数据
        double Calculate(Model models);
    }
}
