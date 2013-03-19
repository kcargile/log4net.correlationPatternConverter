using System.Diagnostics;
using System.IO;
using log4net.Core;

namespace log4net.Layout.Pattern
{
    /// <summary>
    /// <see cref="PatternLayoutConverter"/> for injecting Trace.CorrelationManger.ActivityId into log messages. 
    /// </summary>
    public class CorrelationPatternConverter : PatternLayoutConverter
    {
        /// <summary>
        /// Derived pattern converters must override this method in order to
        /// convert conversion specifiers in the correct way.
        /// </summary>
        /// <param name="writer"><see cref="T:System.IO.TextWriter"/> that will receive the formatted result.</param>
        /// <param name="loggingEvent">The <see cref="T:log4net.Core.LoggingEvent"/> on which the pattern converter should be executed.</param>
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            writer.Write(Trace.CorrelationManager.ActivityId.ToString());
        }
    }
}