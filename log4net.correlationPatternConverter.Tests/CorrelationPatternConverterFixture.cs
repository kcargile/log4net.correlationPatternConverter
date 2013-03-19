using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace log4net.Layout.Pattern.Tests
{
    [TestFixture]
    public class CorrelationPatternConverterFixture
    {
        private const string LogFileName = "unittest.log";
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [TearDown]
        public void TestTeardown()
        {
            if (File.Exists(LogFileName))
            {
                File.Delete(LogFileName);
            }
        }

        [Test]
        public void LogWithCorrelationIdSucceeds()
        {
            Config.XmlConfigurator.Configure();

            Guid expectedId = Guid.NewGuid();

            Trace.CorrelationManager.StartLogicalOperation();
            Trace.CorrelationManager.ActivityId = expectedId;

            string message = BuildTestMessage(MethodBase.GetCurrentMethod().Name);
            Logger.Info(message);
            Logger.Logger.Repository.Shutdown();

            AssertThatLineWasWritten(message, expectedId);

            Trace.CorrelationManager.StopLogicalOperation();
        }

        [Test]
        public void LogWithEmptyCorrelationIdSucceeds()
        {
            Config.XmlConfigurator.Configure();

            Trace.CorrelationManager.StartLogicalOperation();

            string message = BuildTestMessage(MethodBase.GetCurrentMethod().Name);
            Logger.Info(message);
            Logger.Logger.Repository.Shutdown();

            AssertThatLineWasWritten(message, Trace.CorrelationManager.ActivityId); // Guid.Empty

            Trace.CorrelationManager.StopLogicalOperation();
        }

        private string BuildTestMessage(string methodName)
        {
            Debug.Assert(!string.IsNullOrEmpty(methodName));
            return string.Format("Test message from {0}.", methodName);
        }

        private void AssertThatLineWasWritten(string expectedMessage, Guid expectedId)
        {
            Debug.Assert(!string.IsNullOrEmpty(expectedMessage));

            using (FileStream fs = new FileStream(LogFileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(expectedMessage))
                        {
                            StringAssert.Contains(expectedId.ToString(), line);
                        }
                        Console.WriteLine(line);
                    }
                }
            }
        }
    }
}
