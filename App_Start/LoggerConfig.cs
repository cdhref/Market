using System;
using System.Diagnostics;

namespace Market.App_Start
{
    public class LoggerConfig
    {
        public static void Setup() {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
        }
    }
}