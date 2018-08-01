using log4net;
using log4net.Core;
using System;

namespace CommonUtils
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface IProgramLog:ILog,ILoggerWrapper
    {
        /// <summary>
		/// 是否开启跟踪
		/// </summary>
		bool IsTraceEnabled
        {
            get;
        }
        /// <summary>
        /// 跟踪消息
        /// </summary>
        /// <param name="message"></param>
        void Trace(object message);
        /// <summary>
        /// 跟踪消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="t"></param>
        void Trace(object message, Exception t);
        /// <summary>
        /// 跟踪消息
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void TraceFormat(string format, params object[] args);
    }
}
