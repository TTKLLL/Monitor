using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 土压力计
    /// </summary>
    public class TuYa : ICalculateModel
    {
        public TuYa()
        {
            //给常量赋值
            this.K = 8.6960 * Math.Pow(10, -8);

            //给f0赋值
            this.f0 = 1486;
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
