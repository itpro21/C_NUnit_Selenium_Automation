using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using System;
using System.IO;

namespace AdvantageShoppingTests.Utils
{
    public static class TestReportManager
    {
        private static ExtentReports extent;
        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string ReportPath = Path.Combine(projectRoot, "Reports", "ExtentReport.html");
                var sparkReporter = new ExtentSparkReporter(ReportPath);

                sparkReporter.Config.DocumentTitle = "Automation Test Report";
                sparkReporter.Config.ReportName = "Advantage Shopping Tests";
                sparkReporter.Config.Theme = Theme.Standard;

                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
            }
            return extent;
        }
    }
}

