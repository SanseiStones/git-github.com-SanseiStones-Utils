using log4net;
using log4net.Core;
using log4net.Repository;
using System;

namespace CommonUtils
{ 
    /// <summary>
    /// 日志扩展类
    /// </summary>
    internal class ProgramLogImpl:LogImpl,IProgramLog,ILog,ILoggerWrapper
{
        /// <summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        /// </summary>
        private static readonly Type ThisDeclaringType = typeof(ProgramLogImpl);
        /// <summary>
        /// The default value for the TRACE level
        /// </summary>
        private static readonly Level s_defaultLevelTrace = new Level(20000, "TRACE");   
        /// <summary>
        /// The current value for the TRACE level
        /// </summary>
        private Level m_levelTrace;
        /// <summary>
        /// 是否启用Trace
        /// </summary>
        public bool IsTraceEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(this.m_levelTrace);
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public ProgramLogImpl(ILogger logger) : base(logger)
        {
        }
        /// <summary>
        /// Lookup the current value of the TRACE level
        /// </summary>
        protected override void ReloadLevels(ILoggerRepository repository)
        {
            base.ReloadLevels(repository);
            this.m_levelTrace = repository.LevelMap.LookupWithDefault(ProgramLogImpl.s_defaultLevelTrace);
        }
        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="message"></param>
        public void Trace(object message)
        {
            this.Logger.Log(ProgramLogImpl.ThisDeclaringType, this.m_levelTrace, message, null);
        }
        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="t"></param>
        public void Trace(object message, Exception t)
        {
            this.Logger.Log(ProgramLogImpl.ThisDeclaringType, this.m_levelTrace, message, t);
        }
        /// <summary>
        /// Trace Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void TraceFormat(string format, params object[] args)
        {
            this.Logger.Log(ProgramLogImpl.ThisDeclaringType, this.m_levelTrace, string.Format(format, args), null);
        }
    }
}
