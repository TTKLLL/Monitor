using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 孔隙水压计
    /// </summary>
    public class ShuiYa : ICalculateModel
    {
        static DataProcessCommon common = new DataProcessCommon();
        double fi { set; get; }
        double f0 { set; get; }
        double K { set; get; }
        double Kt { set; get; }
        double Ti { set; get; }
        double t0 { set; get; }

        public ShuiYa()
        {
            this.K = -3.3257 * Math.Pow(10, -7);
            this.f0 = 1652;
        }

        public double Calculate(Model model)
        {
            t0 = common.GetT0(model.pointName);
            double res = K * (Math.Pow(model.valueOne, 2) - Math.Pow(f0, 2)) + Kt * (model.valueTwo - t0);
            return res;
        }
    }
}
