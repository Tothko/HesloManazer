using inSight_Logging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSight_Logging.Interface
{
    public interface ILogService
    {
        void Log(string Name, string Message, LogType logType);
        void Log(string Name, string Message);
        void Log(string Message, LogType logType);
        void Log(string Message);

    }
}
