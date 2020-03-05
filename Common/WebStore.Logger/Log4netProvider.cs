using System;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Xml;
using log4net.Layout;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public class Log4netProvider :ILoggerProvider
    {
        private readonly string _ConfigurationFile;
        private readonly ConcurrentDictionary<string, Log4netLogger> _Loggers = new ConcurrentDictionary<string, Log4netLogger>();

        public Log4netProvider(string ConfigurationFile)
        {
            _ConfigurationFile = ConfigurationFile;
        }
        public void Dispose() => _Loggers.Clear();

        public ILogger CreateLogger(string categoryName)
        {
            return _Loggers.GetOrAdd(categoryName, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_ConfigurationFile);
                return new Log4netLogger(category, xml["log4net"]);
            });
        }
    }
}
