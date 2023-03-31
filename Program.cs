using ReportRendererDemo;

namespace TelerikReportRenderer.ReportRenderRunner
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                var reportRendererRunner = ReportRendererRunnerFactory.CreateReportRendererRunner();

                await reportRendererRunner.RenderAllReportsInParallelAsync();

                var lowestRenderTimes = reportRendererRunner.TimeTracker.GetLowestRenderTimes();
                Console.WriteLine("Lowest rendering times for each report:");
                foreach (var reportTime in lowestRenderTimes)
                {
                    Console.WriteLine($"{Path.GetFileName(reportTime.Key)}: {reportTime.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}