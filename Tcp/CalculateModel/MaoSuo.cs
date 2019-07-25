using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 锚索计
    /// </summary>
    public class MaoSuo : ICalculateModel
    {
        public MaoSuo()
        {
            //给常量赋值
            this.K = -3.0587 * Math.Pow(10, -4);
        }

        double fi { set; get; }
        double f0 { set; get; }
        double K { set; get; }

        public double Calculate(Model model)
        {
            double res = 0;
            return res;
        }
    }
}
