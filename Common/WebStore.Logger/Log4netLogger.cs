using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebStore.Logger
{
    public class Log4netLogger:ILogger
    {

        private readonly ILog _Log;
        public Log4netLogger(string CategoryName, XmlElement Configuration)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, CategoryName);
            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuration);
        }
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), logLevel, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _Log.IsDebugEnabled;

                case LogLevel.Information:
                    return _Log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _Log.IsWarnEnabled;

                case LogLevel.Error:
                    return _Log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _Log.IsFatalEnabled;

                case LogLevel.None:
                    return false;
            }
        }
        public void Log<TState>(LogLevel LogLevel, EventId EventId, TState State, Exception error, Func<TState, Exception, string> Formatter)
        {
             if(Formatter is null)
                 throw  new ArgumentNullException(nameof(Formatter));
             if (!IsEnabled(LogLevel)) return;

             var log_message = Formatter(State, error);
             if (string.IsNullOrEmpty(log_message) && error is null) return;
            switch (LogLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), LogLevel, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(log_message);
                    break;

                case LogLevel.Information:
                    _Log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    _Log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    _Log.Error(log_message ?? error.ToString());
                    break;

                case LogLevel.Critical:
                    _Log.Fatal(log_message ?? error.ToString());
                    break;

                case LogLevel.None:
                    break;
            }
        }


    }
}