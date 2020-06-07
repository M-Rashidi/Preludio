using System.Diagnostics;

namespace Preludio.Core.Logging
{
    public static class Logger
    {
        public static ILogger Current { get; private set; }
        public static void SetCurrent(ILogger logger)
        {
            Debug.Assert(Current == null);
            Current = logger;
        }
    }
}
