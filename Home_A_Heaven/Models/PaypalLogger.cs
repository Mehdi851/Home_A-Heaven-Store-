using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Home_A_Heaven.Models
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = Environment.CurrentDirectory;
        public static void Log(String message)
        {
            try
            {
                StreamWriter strw = new StreamWriter(LogDirectoryPath+"\\PaypalError.log",true);
                strw.WriteLine("{0}--->{1}",DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}