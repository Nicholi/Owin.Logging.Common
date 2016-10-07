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
            app.SetLoggerFactory(new CommonLoggingFactory(isLogEventEnabledFunc));
        }

        public static void UseCommonLogging(this IAppBuilder app, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            app.SetLoggerFactory(new CommonLoggingFactory(writeLogEventFunc));
        }

        public static void UseCommonLogging(this IAppBuilder app, Func<ILog, TraceEventType, bool> isLogEventEnabledFunc, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            app.SetLoggerFactory(new CommonLoggingFactory(isLogEventEnabledFunc, writeLogEventFunc));
        }

        public static void UseCommonLogging(this IAppBuilder app, Func<String, ILog> commonLogConstructor, Func<ILog, TraceEventType, bool> isLogEventEnabledFunc, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            app.SetLoggerFactory(new CommonLoggingFactory(commonLogConstructor, isLogEventEnabledFunc, writeLogEventFunc));
        }
    }
}
