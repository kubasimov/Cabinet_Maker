using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreS.Helpers
{
    public class MyLogger
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}
