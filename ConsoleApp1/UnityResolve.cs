using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class UnityResolve<T> where T : class
    {
        private static T Instance;
        static UnityResolve()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            //      fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config.xml");//找配置文件的路径
            fileMap.ExeConfigFilename = Path.Combine(Environment.CurrentDirectory, "CfgFiles", "Unity.Config.xml");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            IUnityContainer container = new UnityContainer();
            section.Configure(container, "Homework11Container");

            Instance = container.Resolve<T>();
        }
        public static T GetInstance()
        {
            return Instance;
        }
    }
}