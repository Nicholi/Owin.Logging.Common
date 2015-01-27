using System;
using System.Diagnostics;
using Common.Logging;
using Microsoft.Owin.Logging;

namespace Owin.Logging.Common
{
    public class CommonLoggingFactory : ILoggerFactory
    {
        private readonly Func<ILog, TraceEventType, bool> m_IsLogEventEnabledFunc;
        private readonly Action<ILog, TraceEventType, String, Exception> m_WriteLogEventFunc;

        public CommonLoggingFactory()
            : this(DefaultIsLogEventEnabled, DefaultWriteLogEvent)
        {
        }

        public CommonLoggingFactory(Func<ILog, TraceEventType, bool> isLogEventEnabledFunc)
            : this(isLogEventEnabledFunc, DefaultWriteLogEvent)
        {
        }

        public CommonLoggingFactory(Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
            : this(DefaultIsLogEventEnabled, writeLogEventFunc)
        {
        }

        public CommonLoggingFactory(Func<ILog, TraceEventType, bool> isLogEventEnabledFunc, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            if (isLogEventEnabledFunc == null)
            {
                throw new ArgumentNullException("isLogEventEnabledFunc");
            }
            if (writeLogEventFunc == null)
            {
                throw new ArgumentNullException("writeLogEventFunc");
            }

            m_IsLogEventEnabledFunc = isLogEventEnabledFunc;
            m_WriteLogEventFunc = writeLogEventFunc;
        }

        public ILogger Create(string name)
        {
            return new CommonLogger(LogManager.GetLogger(name), DefaultIsLogEventEnabled, DefaultWriteLogEvent);
        }

        static bool DefaultIsLogEventEnabled(ILog log, TraceEventType traceEventType)
        {
            switch (traceEventType)
            {
                case TraceEventType.Critical:
                    return log.IsFatalEnabled;
                case TraceEventType.Error:
                    return log.IsErrorEnabled;
                case TraceEventType.Warning:
                    return log.IsWarnEnabled;
                case TraceEventType.Information:
                    return log.IsInfoEnabled;
                case TraceEventType.Verbose:
                    return log.IsTraceEnabled;
                case TraceEventType.Start:
                    return log.IsDebugEnabled;
                case TraceEventType.Stop:
                    return log.IsDebugEnabled;
                case TraceEventType.Suspend:
                    return log.IsDebugEnabled;
                case TraceEventType.Resume:
                    return log.IsDebugEnabled;
                case TraceEventType.Transfer:
                    return log.IsDebugEnabled;
                default:
                    throw new ArgumentOutOfRangeException("traceEventType");
            }
        }

        static void DefaultWriteLogEvent(ILog log, TraceEventType traceEventType, String message, Exception ex)
        {
            switch (traceEventType)
            {
                case TraceEventType.Critical:
                    log.FatalFormat(message, ex);
                    break;
                case TraceEventType.Error:
                    log.ErrorFormat(message, ex);
                    break;
                case TraceEventType.Warning:
                    log.WarnFormat(message, ex);
                    break;
                case TraceEventType.Information:
                    log.InfoFormat(message, ex);
                    break;
                case TraceEventType.Verbose:
                    log.TraceFormat(message, ex);
                    break;
                case TraceEventType.Start:
                    log.DebugFormat(message, ex);
                    break;
                case TraceEventType.Stop:
                    log.DebugFormat(message, ex);
                    break;
                case TraceEventType.Suspend:
                    log.DebugFormat(message, ex);
                    break;
                case TraceEventType.Resume:
                    log.DebugFormat(message, ex);
                    break;
                case TraceEventType.Transfer:
                    log.DebugFormat(message, ex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("traceEventType");
            }
        }
    }
}
