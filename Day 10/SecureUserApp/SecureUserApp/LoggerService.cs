using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SecureUserApp
{
    public static class LoggerService
    {
        private static readonly string LogFilePath = "app_log.txt";

        public static void Log(string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now}] {message}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, logEntry);
            }
            catch (Exception)
            {

            }
        }
    }
}
