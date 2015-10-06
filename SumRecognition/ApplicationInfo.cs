using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumRecognition
{
    public class ApplicationInfo
    {
        public string appPath { get; private set; }
        private static ApplicationInfo _instance = null;
        public static ApplicationInfo instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationInfo();
                }
                return _instance;
            }
        }

        private ApplicationInfo()
        {
            appPath = AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
