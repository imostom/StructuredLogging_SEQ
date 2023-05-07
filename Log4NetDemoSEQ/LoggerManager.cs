
using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml;

namespace Log4NetDemoSEQ
{
    public interface ILoggerManager
    {
        void Information(string message);
        void Debug(string message);
        void Warning(string message);
        void Error(object message, Exception exception);
        void Error(string message, Exception exception);
    }


    public class LoggerManager : ILoggerManager
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
        public LoggerManager()
        {
            try
            {
                XmlDocument log4netConfig = new XmlDocument();

                using (var fs = File.OpenRead("log4net.config"))
                {
                    log4netConfig.Load(fs);

                    var repo = LogManager.CreateRepository(
                            Assembly.GetEntryAssembly(),
                            typeof(log4net.Repository.Hierarchy.Hierarchy));

                    XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

                    // The first log to be written 
                    _logger.Info("Log System Initialized");
                }

                //XmlConfigurator.ConfigureAndWatch(new FileInfo(@"\log4net.config"));
            }
            catch (Exception ex)
            {
                _logger.Error("Error", ex);
            }
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }
        public void Information(string message)
        {
            _logger.InfoFormat(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Error(object message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }
    }

}
