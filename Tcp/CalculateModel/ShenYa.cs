using System;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 渗压计
    /// </summary>
    public class ShenYa : ICalculateModel
    {
        static DataProcessCommon common = new DataProcessCommon();
        public double K { set; get; }
        public double f0 { set; get; }
        public double fi { set; get; }
        public double Kt { set; get; }
        public double Ti { set; get; }
        public double t0 { set; get; }

        public ShenYa()
        {
            this.K = -3.3257 * Math.Pow(10, -7);
            this.f0 = 1652;
        }

        public double Calculate(Model model)
        {
            this.t0 = common.GetT0(model.pointName);
            double res = this.K * (Math.Pow(model.valueOne, 2) - Math.Pow(this.f0, 2)) + this.Kt * (model.valueTwo - this.t0);
            return res;
        }
    }
}
