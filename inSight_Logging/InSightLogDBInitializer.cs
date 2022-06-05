using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSight_Logging
{
    public class InSightLogDBInitializer
    {
        public static void SeedDB(InSightLogDatabaseContext ctx)
        {
            ctx.Database.Exists();

        }
    }
}
