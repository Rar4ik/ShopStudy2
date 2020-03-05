using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4netExtentions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory loggerFactory, string ConfigurationFile = "log4net.config")
        {
            if (!Path.IsPathRooted(ConfigurationFile))
            {
                var assembly = Assembly.GetEntryAssembly()
                               ?? throw new InvalidOperationException("не удалось определитьсборку с точкой входа");
                var dir = Path.GetDirectoryName(assembly.Location)
                          ?? throw new InvalidOperationException("не удалось опрежедить каталог исполнительного файла");
                ConfigurationFile = Path.Combine(dir, ConfigurationFile);
            }

            loggerFactory.AddProvider(new Log4netProvider(ConfigurationFile));

            return loggerFactory;
        }
    }
}