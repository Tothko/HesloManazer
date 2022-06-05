using Effort;
using inSight_Logging.Interface;
using inSight_Logging.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSight_Logging.Repositories
{
    internal static class LogRepository 
    {
        static DbConnection connection = DbConnectionFactory.CreateTransient();
        private static InSightLogDatabaseContext ctx = new InSightLogDatabaseContext(connection);

        public static void Log(LogObject log)
        {
            var inputLog = log;
            switch (log.Source)
            {
                case LogSource.Default:
                    DefaultLog defaultLog = inputLog as DefaultLog;
                    ctx.DefaultLog.Local.Add(defaultLog);
                    break;
                case LogSource.Client:
                    var clientLog = (ClientLog)inputLog;
                    ctx.ClientLog.Local.Add(clientLog);

                    break;
                case LogSource.Api:
                    var apiLog = (ApiLog)inputLog;
                    ctx.ApiAccessLog.Local.Add(apiLog);

                    break;
                case LogSource.DataAccess:
                    var dataAccessLog = (DataAccessLog)inputLog;
                    ctx.DatabaseLog.Local.Add(dataAccessLog);

                    break;
                default:
                    break;
            }

        }

        public static List<LogObject> GetAllLogs(LogSource logSource)
        {
            List< LogObject> generalizedList;
            switch (logSource)
            {
                case LogSource.Default:
                    var defaultLogs = ctx.DefaultLog.Local;
                    generalizedList = defaultLogs.Cast<LogObject>().ToList();
                    return generalizedList;


                case LogSource.Client:
                    var clientLogs = ctx.ClientLog.ToList();
                    generalizedList = clientLogs.Cast<LogObject>().ToList();
                    return generalizedList;

                case LogSource.Api:
                    var apiLogs = ctx.ApiAccessLog.ToList();
                     generalizedList = apiLogs.Cast<LogObject>().ToList();
                    return generalizedList;

                case LogSource.DataAccess:
                    var dataAccessLogs = ctx.DatabaseLog.ToList();
                    generalizedList = dataAccessLogs.Cast<LogObject>().ToList();
                    return generalizedList;

                default:
                    return new List<LogObject>();          
                        }

        }

        public static LogObject GetOneLog(int Id, LogSource logSource)
        {
           
            switch (logSource)
            {
                case LogSource.Default:
                   return ctx.DefaultLog.FirstOrDefault(x => x.ID == Id);                   
                case LogSource.Client:
                    return ctx.ClientLog.FirstOrDefault(x => x.ID == Id);
                case LogSource.Api:
                    return ctx.ApiAccessLog.FirstOrDefault(x => x.ID == Id);
                case LogSource.DataAccess:
                    return ctx.DatabaseLog.FirstOrDefault(x => x.ID == Id);
                default:
                    return new LogObject();
            }

        }

    }
}
