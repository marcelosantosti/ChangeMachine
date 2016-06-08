using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Logger
{
    static class LogManager
    {
        public static void Info(string message, object obj, [CallerMemberName] string memberName = "")
        {
            Log(message, obj, LogLevel.INFO, memberName);
        }

        public static void Info(object obj, [CallerMemberName] string memberName = "")
        {
            Info("", obj, memberName);
        }

        public static void Error(string message, object obj = null, [CallerMemberName] string memberName = "")
        {
            Log(message, obj, LogLevel.ERROR, memberName);
        }

        private static void Log(string message, object obj, LogLevel level, string memberName)
        {
            AbstractLogger[] loggerCollection = LoggerFactory.Create(level, obj);
            foreach (AbstractLogger logger in loggerCollection)
            {
                logger.Log(message, level, obj, memberName);
            }
        }
    }
}
