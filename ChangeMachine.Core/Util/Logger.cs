using ChangeMachine.Core.DataContract;
using Dlp.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace ChangeMachine.Core.Util
{
    internal class Logger
    {
        private const string LOG_PATH = @"C:\Logs\";
        private const string LOG_FILE = @"ChangeMachine.log";

        private static Logger logger;
        private StreamWriter logWriter;

        public static Logger Instance
        {
            get
            {
                if(logger == null)
                {
                    logger = new Logger();
                }
                return logger;
            }
        }

        public Logger()
        {

        }

        private void Log(string message)
        {
            if (Directory.Exists(LOG_PATH) == false)
            {
                Directory.CreateDirectory(LOG_PATH);
            }

            File.AppendAllText(Path.Combine(LOG_PATH, LOG_FILE), string.Format("{0}{1}", message, Environment.NewLine));
        }

        private void Log(string method, string message)
        {
            string logMessage = string.Format("{0} {1} {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), method, message);
            this.Log(logMessage);
        }

        private string FormatMessage(string level, string message, object obj = null)
        {
            if (obj == null)
            {
                return string.Format("{0}: {1}", level, message);
            }

            string serializedObject = Serializer.NewtonsoftSerialize(obj);
            string objType = string.Empty;

            if (obj is AbstractRequest)
            {
                objType = "Request";
            }
            else if (obj is AbstractResponse)
            {
                objType = "Response";
            }
            else if (obj is Exception)
            {
                objType = "EXCEPTION";
            }

            if(string.IsNullOrEmpty(objType))
            {
                return string.Format("{0}: {1} [{2}]", level, message, serializedObject); 
            }
            else
            {
                return string.Format("{0}: [{1}] {2} [{3}]", level, objType, message, serializedObject); 
            }
        }

        public void Info(string message, object obj, [CallerMemberName] string memberName = "")
        {
            this.Log(memberName, FormatMessage("INFO", message, obj));
        }

        public void Info(object obj, [CallerMemberName] string memberName = "")
        {
            this.Log(memberName, FormatMessage("INFO", "", obj));
        }

        public void Error(string message, object obj = null, [CallerMemberName] string memberName = "")
        {
            this.Log(memberName, this.FormatMessage("ERROR", message, obj));
        }
    }
}
