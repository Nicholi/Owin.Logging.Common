using System;
using System.Diagnostics;
using Common.Logging;
using Microsoft.Owin.Logging;

namespace Owin.Logging.Common
{
    public class CommonLogger : ILogger
    {
        private readonly ILog m_CommonLog;
        private readonly Func<ILog, TraceEventType, bool> m_IsLogEventEnabledFunc;
        private readonly Action<ILog, TraceEventType, String, Exception> m_WriteLogEventFunc;

        public CommonLogger(ILog commonLog, Func<ILog, TraceEventType, bool> isLogEventEnabledFunc, Action<ILog, TraceEventType, String, Exception> writeLogEventFunc)
        {
            if (commonLog == null)
            {
                throw new ArgumentNullException("commonLog");
            }
            if (isLogEventEnabledFunc == null)
            {
                throw new ArgumentNullException("isLogEventEnabledFunc");
            }
            if (writeLogEventFunc == null)
            {
                throw new ArgumentNullException("writeLogEventFunc");
            }

            m_CommonLog = commonLog;
            m_IsLogEventEnabledFunc = isLogEventEnabledFunc;
            m_WriteLogEventFunc = writeLogEventFunc;
        }

        public bool WriteCore(TraceEventType traceEventType, int eventId, Object state, Exception exception, Func<Object, Exception, String> formatter)
        {
            /// http://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin/Logging/ILogger.cs
            /// To check IsEnabled call WriteCore with only TraceEventType and check the return value, no event will be written.
            if (state == null)
            {
                return m_IsLogEventEnabledFunc(m_CommonLog, traceEventType);
            }

            // no need to continue if event type isn't enabled
            if (!m_IsLogEventEnabledFunc(m_CommonLog, traceEventType))
            {
                return false;
            }

            m_WriteLogEventFunc(m_CommonLog, traceEventType, formatter(state, exception), exception);
            return true;
        }
    }
}
