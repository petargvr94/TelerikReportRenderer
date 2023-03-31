using TelerikReportRenderer.ReportTimeTrackers;

namespace TelerikReportRenderer.ReportRenderRunner
{
    public interface IReportRendererRunner
    {
        int NumberOfRenderings { get; set; }
        IReportTimeTracker TimeTracker { get; set; }

        Task RenderAllReportsInParallelAsync();
        IReadOnlyDictionary<string, TimeSpan> GetLowestRenderTimes();
    }
}
