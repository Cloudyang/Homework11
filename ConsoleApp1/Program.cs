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
        private static IBaseService _Service;
        static Program()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            //      fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config.xml");//找配置文件的路径
            fileMap.ExeConfigFilename = Path.Combine(Environment.CurrentDirectory, "CfgFiles", "Unity.Config.xml");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            IUnityContainer container = new UnityContainer();
            section.Configure(container, "Homework11Container");

            _Service = container.Resolve<IBaseService>();
        }
        static void Main(string[] args)
        {
            #region 测试部分增、删、改、查

            #endregion
        }
    }
}
