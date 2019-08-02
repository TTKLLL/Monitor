using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   /// <summary>
   /// 菜单
   /// </summary>
    public class MenueModel
    {
        public int id { set; get; }
        public string name { set; get; }
        public int parentId { set; get; }
    }
}
