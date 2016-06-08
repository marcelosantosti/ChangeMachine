using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Logger
{
    internal static class LoggerFactory
    {
        public static AbstractLogger[] Create(LogLevel level, object obj = null)
        {
            List<AbstractLogger> loggerCollection = new List<AbstractLogger>();

            // Always log to file
            loggerCollection.Add(new FileLogger());

            // Log Exceptions to EventLog
            if(obj is Exception)
            {
                loggerCollection.Add(new EventLogger());
            }

            return loggerCollection.ToArray();
        }
    }
}
