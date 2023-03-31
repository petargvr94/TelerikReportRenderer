using System.Collections.Concurrent;

namespace TelerikReportRenderer.ReportTimeTrackers
{
    public class ReportTimeTracker : IReportTimeTracker
    {
        private readonly ConcurrentDictionary<string, TimeSpan> _lowestRenderTimes = new ConcurrentDictionary<string, TimeSpan>();

        public void TrackTime(string reportPath, TimeSpan renderTime)
        {
            _lowestRenderTimes.AddOrUpdate(reportPath, renderTime, (key, existingValue) => renderTime < existingValue ? renderTime : existingValue);
        }

        public IReadOnlyDictionary<string, TimeSpan> GetLowestRenderTimes()
        {
            return _lowestRenderTimes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}