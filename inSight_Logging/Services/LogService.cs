using inSight_Logging.Interface;
using inSight_Logging.Model;
using inSight_Logging.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSight_Logging.Services
{
    public static class LogService
    {

        public static void Log(string Name, string Message, LogType logType)
        {
            LogObject newLog = new DefaultLog
            {
                Name = Name,
                DateRecorded = DateTime.Now,
                Message = Message,
                Type = logType,
                Source = LogSource.Default
            };
            LogRepository.Log(newLog);
        }

        public static void Log(string Name, string Message)
        {
            LogObject newLog = new DefaultLog
            {
                Name = Name,
                DateRecorded = DateTime.Now,
                Message = Message,
                Type = LogType.Warning,
                Source = LogSource.Default
            };
            LogRepository.Log(newLog);
        }

        public static void Log(string Message, LogType logType)
        {
            LogObject newLog = new DefaultLog
            {
                Name = "Unnnamed",
                DateRecorded = DateTime.Now,
                Message = Message,
                Type = logType,
                Source = LogSource.Default
            };
            LogRepository.Log(newLog);
        }

        public static void Log(string Message)
        {
            LogObject newLog = new DefaultLog
            {
                Name = "Unnamed",
                DateRecorded = DateTime.Now,
                Message = Message,
                Type = LogType.Warning,
                Source = LogSource.Default
            };
            LogRepository.Log(newLog);
        }
        public static List<LogObject> GetAllLogs(LogSource logSource)
        {
            return LogRepository.GetAllLogs(logSource);
        }

        public static LogObject GetOneLog(LogObject log)
        {

            return LogRepository.GetOneLog(log.ID, log.Source);

        }

    }
}
