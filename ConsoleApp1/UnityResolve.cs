using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
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
            fileMap.ExeConfigFilename = Path.Combine(Environment.CurrentDirectory, "CfgFiles", "Unity.Config.xml");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            IUnityContainer container = new UnityContainer();
            section.Configure(container, "Homework11ContainerAOP");
            Instance = container.Resolve<T>();
        }
        public static T GetInstance()
        {
            return Instance;
        }
    }
}