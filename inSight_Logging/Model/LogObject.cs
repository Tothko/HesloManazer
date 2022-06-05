using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSight_Logging.Model
{
    public class LogObject
    {
        public int ID { get; set; }
        public DateTime DateRecorded { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public LogType Type { get; set; }
        public LogSource Source { get; set; }
    }
    public enum LogType
    {
        Info = 0,
        Warning = 1,
        ERROR = 2,
        FATAL = 3

    }
    public enum LogSource
    {
        Default = 0,
        Client = 1,
        Api = 2,
        DataAccess = 3,
        

    }
}
