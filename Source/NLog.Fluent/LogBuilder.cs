using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NLog.Fluent
{
    /// <summary>
    /// A fluent class to build log events for NLog.
    /// </summary>
    public class LogBuilder
    {
        private readonly LogEventInfo _logEvent;
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogBuilder"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="Logger"/> to send the log event.</param>
        public LogBuilder(Logger logger)
            : this(logger, LogLevel.Debug)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogBuilder"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="Logger"/> to send the log event.</param>
        /// <param name="logLevel">The <see cref="LogLevel"/> for the log event.</param>
        public LogBuilder(Logger logger, LogLevel logLevel)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (logLevel == null)
                throw new ArgumentNullException("logLevel");

            _logger = logger;
            _logEvent = new LogEventInfo
            {
                Level = logLevel,
                LoggerName = logger.Name,
                TimeStamp = DateTime.Now
            };
        }

        /// <summary>
        /// Gets the <see cref="LogEventInfo"/> created by the builder.
        /// </summary>
        public LogEventInfo LogEventInfo
        {
            get { return _logEvent; }
        }

        /// <summary>
        /// Sets the exception information of the logging event.
        /// </summary>
        /// <param name="exception">The exception information of the logging event.</param>
        /// <returns></returns>
        public LogBuilder Exception(Exception exception)
        {
            _logEvent.Exception = exception;
            return this;
        }

        /// <summary>
        /// Sets the level of the logging event.
        /// </summary>
        /// <param name="logLevel">The level of the logging event.</param>
        /// <returns></returns>
        public LogBuilder Level(LogLevel logLevel)
        {
            if (logLevel == null)
                throw new ArgumentNullException("logLevel");

            _logEvent.Level = logLevel;
            return this;
        }

        /// <summary>
        /// Sets the logger name of the logging event.
        /// </summary>
        /// <param name="loggerName">The logger name of the logging event.</param>
        /// <returns></returns>
        public LogBuilder LoggerName(string loggerName)
        {
            if (loggerName == null)
                throw new ArgumentNullException("loggerName");

            _logEvent.LoggerName = loggerName;
            return this;
        }

        /// <summary>
        /// Sets the log message on the logging event.
        /// </summary>
        /// <param name="message">The log message for the logging event.</param>
        /// <returns></returns>
        public LogBuilder Message(string message)
        {
            _logEvent.Message = message;

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0)
        {
            _logEvent.Message = format;
            _logEvent.Parameters = new[] { arg0 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0, object arg1)
        {
            _logEvent.Message = format;
            _logEvent.Parameters = new[] { arg0, arg1 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0, object arg1, object arg2)
        {
            _logEvent.Message = format;
            _logEvent.Parameters = new[] { arg0, arg1, arg2 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <param name="arg3">The fourth object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
        {
            _logEvent.Message = format;
            _logEvent.Parameters = new[] { arg0, arg1, arg2, arg3 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, params object[] args)
        {
            _logEvent.Message = format;
            _logEvent.Parameters = args;

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public LogBuilder Message(IFormatProvider provider, string format, params object[] args)
        {
            _logEvent.FormatProvider = provider;
            _logEvent.Message = format;
            _logEvent.Parameters = args;

            return this;
        }

        /// <summary>
        /// Sets a per-event context property on the logging event.
        /// </summary>
        /// <param name="name">The name of the context property.</param>
        /// <param name="value">The value of the context property.</param>
        /// <returns></returns>
        public LogBuilder Property(object name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _logEvent.Properties[name] = value;
            return this;
        }

        /// <summary>
        /// Sets the timestamp of the logging event.
        /// </summary>
        /// <param name="timeStamp">The timestamp of the logging event.</param>
        /// <returns></returns>
        public LogBuilder TimeStamp(DateTime timeStamp)
        {
            _logEvent.TimeStamp = timeStamp;
            return this;
        }

        /// <summary>
        /// Sets the stack trace for the event info.
        /// </summary>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="userStackFrame">Index of the first user stack frame within the stack trace.</param>
        /// <returns></returns>
        public LogBuilder StackTrace(StackTrace stackTrace, int userStackFrame)
        {
            _logEvent.SetStackTrace(stackTrace, userStackFrame);
            return this;
        }

        /// <summary>
        /// Writes the log event to the underlying logger.
        /// </summary>
        /// <param name="callerMemberName">The method or property name of the caller to the method. This is set at by the compiler.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller. This is set at by the compiler.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called. This is set at by the compiler.</param>
        public void Write(
            [CallerMemberName]string callerMemberName = null,
            [CallerFilePath]string callerFilePath = null,
            [CallerLineNumber]int callerLineNumber = 0)
        {
            if (callerMemberName != null)
                Property("CallerMemberName", callerMemberName);
            if (callerFilePath != null)
                Property("CallerFilePath", callerFilePath);
            if (callerLineNumber != 0)
                Property("CallerLineNumber", callerLineNumber);

            _logger.Log(_logEvent);
        }
    }
}