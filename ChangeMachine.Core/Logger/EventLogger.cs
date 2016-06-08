using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChangeMachine.Core.Logger
{
    class EventLogger : AbstractLogger
    {
        private const string source = "Change Machine";
        private const string log = "Application";

        public EventLogger()
        {

        }

        protected override void Log(string message, LogLevel level)
        {
            try
            {
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, log);

                EventLog.WriteEntry(source, message, GetEventType(level));
            }
            catch(Exception ex)
            {
                // Sorry, can't log...
            }
        }

        private EventLogEntryType GetEventType(LogLevel level)
        {
            switch(level)
            {
                case LogLevel.INFO:
                    return EventLogEntryType.Information;
                case LogLevel.ERROR:
                    return EventLogEntryType.Error;
            }
            return EventLogEntryType.Information;
        }
    }
}
