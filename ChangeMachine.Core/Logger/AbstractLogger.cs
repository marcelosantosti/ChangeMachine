using ChangeMachine.Core.DataContract;
using Dlp.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace ChangeMachine.Core.Logger
{
    public enum LogLevel { INFO, ERROR }

    internal abstract class AbstractLogger
    {
        protected abstract void Log(string message, LogLevel level);

        private void Log(string method, string message, LogLevel level)
        {
            string logMessage = string.Format("{0} {1} {2}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), method, message);
            this.Log(logMessage, level);
        }

        private string FormatMessage(string message, object obj = null)
        {
            if (obj == null)
            {
                return string.Format("{0}", message);
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
                return string.Format("{0} [{1}]", message, serializedObject); 
            }
            else
            {
                return string.Format("[{0}] {1} [{2}]", objType, message, serializedObject); 
            }
        }

        public void Info(string message, object obj, string memberName = "")
        {
            this.Log(memberName, FormatMessage(message, obj), LogLevel.INFO);
        }

        public void Info(object obj, string memberName = "")
        {
            this.Log(memberName, FormatMessage("", obj), LogLevel.INFO);
        }

        public void Error(string message, object obj = null, string memberName = "")
        {
            this.Log(memberName, this.FormatMessage(message, obj), LogLevel.ERROR);
        }

        public void Log(string message, LogLevel level, object obj = null, string memberName = "")
        {
            switch (level)
            {
                case LogLevel.INFO:
                    this.Info(message, obj, memberName);
                    break;
                case LogLevel.ERROR:
                    this.Error(message, obj, memberName);
                    break;
            }
        }
    }
}
