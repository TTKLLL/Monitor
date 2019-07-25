using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 钢筋计
    /// </summary>
    public class GangJin : ICalculateModel
    {
        public GangJin()
        {
            //给常量赋值
            this.K = -8.4845 * Math.Pow(10, -5);

            //给f0赋值
        }

        double fi { set; get; }
        double f0 { set; get; }
        double K { set; get; }

        public double Calculate(Model model)
        {
            double res = this.K * (Math.Pow(model.valueOne, 2) - Math.Pow(this.f0, 2));
            return res;
        }
    }
}
