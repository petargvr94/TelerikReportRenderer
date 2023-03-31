namespace TelerikReportRenderer.ReportTimeTrackers
{
    public interface IReportTimeTracker
    {
        void TrackTime(string reportPath, TimeSpan renderTime);
        IReadOnlyDictionary<string, TimeSpan> GetLowestRenderTimes();
    }
}
