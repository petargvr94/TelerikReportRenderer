using System.Collections;
using System.Diagnostics;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace TelerikReportRenderer.ReportRenderers
{
    internal class PdfReportRenderer : IReportRenderer
    {
        public string ReportPath { get; }

        public PdfReportRenderer(string reportPath)
        {
            ReportPath = reportPath;
        }

        public async Task<TimeSpan> RenderAsync()
        {
            var reportProcessor = new ReportProcessor();
            var reportSource = new UriReportSource { Uri = ReportPath };
            var deviceInfo = new Hashtable();

            var stopwatch = Stopwatch.StartNew();
            reportProcessor.RenderReport("PDF", reportSource, deviceInfo);
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }
    }

}
