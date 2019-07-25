using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp.CalculateModel
{
    /// <summary>
    /// 内埋应变计
    /// </summary>
    public class YingBian : ICalculateModel
    {
        public YingBian()
        {
            //给常量赋值
            this.K = 1.448 * Math.Pow(10, -3);
        }

        static DataProcessCommon common = new DataProcessCommon();
        double fi { set; get; }
        double f0 { set; get; }
        double K { set; get; }
        double Kt { set; get; }
        double tempera { set; get; }
        double t0 { set; get; }

        //计算公式
        public double Calculate(Model model)
        {
            //获取该监测点的t0
            this.t0 = common.GetT0(model.pointName);
            double res = this.K * (Math.Pow(model.valueOne, 2) - Math.Pow(this.f0, 2)) + this.Kt * (model.valueTwo - this.t0);
            return res;
        }

    }
}

