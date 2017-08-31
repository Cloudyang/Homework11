using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using Bussiness.Interface;

namespace ConsoleApp1
{
    class Program
    {
        private static IUserMenuService _UserMenuService = UnityResolve<IUserMenuService>.GetInstance();
        static void Main(string[] args)
        {
            #region 测试部分增、删、改、查
            _UserMenuService.DeleteMenu(1);
            #endregion
        }
    }
}
