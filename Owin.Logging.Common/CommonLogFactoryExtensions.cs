using System;
using System.Diagnostics;
using Common.Logging;
using Microsoft.Owin.Logging;

namespace Owin.Logging.Common
{
    public static class CommonLogFactoryExtensions
    {
        public static void UseCommonLogging(this IAppBuilder app)
        {
            app.SetLoggerFactory(new CommonLoggingFactory());
        }

        public static void UseCommonLogging(this IAppBuilder app, Func<ILog, TraceEventType, bool> isLogEventEnabledFunc)
        {
            UseCommonLogging(app, isLogEventEnabledFunc);
        }

        public static void UseCommonLogging(this IAppBuilder app, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            UseCommonLogging(app, writeLogEventFunc);
        }

        public static void UseCommonLogging(this IAppBuilder app, Func<ILog, TraceEventType, bool> isLogEventEnabledFunc, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            app.SetLoggerFactory(new CommonLoggingFactory(isLogEventEnabledFunc, writeLogEventFunc));
        }
    }
}
