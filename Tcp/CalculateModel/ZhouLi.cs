using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 轴力计
    /// </summary>
    public class ZhouLi : ICalculateModel
    {
        double fi { set; get; }
        double f0 { set; get; }
        double K { set; get; }

        public ZhouLi()
        {
            this.K = -1.5888 * Math.Pow(10, -3);
        }

        public double Calculate(Model model)
        {
            double res = this.K * (Math.Pow(model.valueOne, 2) - Math.Pow(this.f0, 2));
            return res;
        }
    }
}
