using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ShortCuts.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ConsoleTarget { Name = "console", Layout = "${longdate} | ${level:uppercase = true} | ${logger} | ${message}" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget, "*");
            NLog.LogManager.Configuration = config;
        }
    }
}
