using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Logger
{
    class FileLogger : AbstractLogger
    {
        protected const string LOG_PATH = @"C:\Logs\";
        protected const string LOG_FILE = @"ChangeMachine.log";

        protected override void Log(string message, LogLevel level)
        {
            if (Directory.Exists(LOG_PATH) == false)
            {
                Directory.CreateDirectory(LOG_PATH);
            }

            File.AppendAllText(Path.Combine(LOG_PATH, LOG_FILE), string.Format("{0} {1}{2}", level, message, Environment.NewLine));
        }
    }
}
