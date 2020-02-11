using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class LogManager
    {
        public static LogManager Self;

        private string SolutionName { get; set; }
        private TextWriter LogWriter { get; set; }

        private LogManager(string solutionName)
        {
            Self = this;

            this.SolutionName = solutionName;

            string logPath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, this.SolutionName);

            Directory.CreateDirectory(logPath);

            logPath = Path.Combine(CppCodeAnalysis.RsAnalyticsDir, this.SolutionName, "Analytics.log");

            this.LogWriter = File.CreateText(logPath);
        }

        public void Log(string messageStr, Exception ex)
        {
            this.LogWriter.WriteLine($"{messageStr} - {ex.Message}");
            this.LogWriter.Flush();
        }

        public void Log(string messageStr)
        {
            this.LogWriter.WriteLine(messageStr);
            this.LogWriter.Flush();
        }

        public void Close()
        {
            this.LogWriter.Flush();
            this.LogWriter.Close();
        }

        public static void Start(string solutionName)
        {
            new LogManager(solutionName);
        }
    }
}
