using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public interface ILogger
    {
        void LogDebug(string context, string scope, params object[] objects);

        void LogInfo(string context, string scope, params object[] objects);

        void LogWarning(string context, string scope, params object[] objects);

        void LogError(string context, string scope, params object[] objects);
    }
}
