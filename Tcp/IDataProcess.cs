using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcp
{
    public interface IDataProcess
    {
        //保存数据
        bool AnalysisData(string data, string port, string dataType, int xmno=0);
    }
}
