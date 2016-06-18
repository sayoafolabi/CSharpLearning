using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutotraderPageObjectModel.AutotraderBase
{

    internal class ExtentManager
    {
         static string dateNow = DateTime.Now.Date.ToString().Replace(@"/", "").Replace(@" ", "").Replace(@":", "");
         static string dateN = dateNow.Substring(0, 8);

         static string timeNow = DateTime.Now.TimeOfDay.ToString().Replace(@"/", "").Replace(@" ","").Replace(@":","").Replace(@".","");
         static string timeN = timeNow.Substring(0, 6);

        private static readonly ExtentReports _instance =
            new ExtentReports(String.Format("C:/Users/Sayo/Documents/Report/report_{0}_{1}.html", dateN, timeN), DisplayOrder.OldestFirst);

        static ExtentManager() { }

        private ExtentManager() { }

        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
