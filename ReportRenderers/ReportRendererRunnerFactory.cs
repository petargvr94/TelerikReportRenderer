using Microsoft.Extensions.DependencyInjection;
using TelerikReportRenderer.ReportRenderers;
using TelerikReportRenderer.ReportRenderRunner;
using TelerikReportRenderer.ReportTimeTrackers;

namespace ReportRendererDemo
{
    internal static class ReportRendererRunnerFactory
    {
        private const string ReportsFolderPath = "Reports";
        private const string ReportFileExtension = ".trdp";

        internal static IReportRendererRunner CreateReportRendererRunner()
        {
            // Get report file names from the Reports folder
            var reportNames = Directory.GetFiles(ReportsFolderPath, $"*{ReportFileExtension}")
                .Select(Path.GetFileName)
                .ToList();

            var serviceCollection = new ServiceCollection();

            // Loop through the report names and register each PdfReportRenderer
            foreach (var reportName in reportNames)
            {
                serviceCollection.AddTransient<IReportRenderer, PdfReportRenderer>(provider =>
                    new PdfReportRenderer(Path.Combine(ReportsFolderPath, reportName)));
            }

            // Register other services
            serviceCollection
                .AddTransient<IReportRendererRunner>(provider =>
                    new ReportRendererRunner(10, provider.GetServices<IReportRenderer>().ToList().AsReadOnly(), new ReportTimeTracker()))
                .AddSingleton<IReportTimeTracker, ReportTimeTracker>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get the IReportRendererRunner instance
            return serviceProvider.GetRequiredService<IReportRendererRunner>();
        }
    }
}