﻿using NLog;
using SecurityDemo.Domain.Logging;
using System;

namespace SecurityDemo.Common.Logging
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
