using System.Diagnostics;
using TelerikReportRenderer.ReportRenderers;
using TelerikReportRenderer.ReportTimeTrackers;

namespace TelerikReportRenderer.ReportRenderRunner
{
    public class ReportRendererRunner : IReportRendererRunner
    {
        private int _numberOfRenderings;
        private List<IReportRenderer> _renderers;
        private IReportTimeTracker _timeTracker;

        public ReportRendererRunner(int numberOfRenderings, IReadOnlyList<IReportRenderer> renderers, IReportTimeTracker timeTracker)
        {
            NumberOfRenderings = numberOfRenderings;
            _renderers = new List<IReportRenderer>(renderers);
            TimeTracker = timeTracker;
        }

        public int NumberOfRenderings
        {
            get { return _numberOfRenderings; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(NumberOfRenderings), "Number of renderings must be greater than or equal to 1.");
                }
                _numberOfRenderings = value;
            }
        }

        public IReportTimeTracker TimeTracker
        {
            get { return _timeTracker; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(TimeTracker), "Time tracker cannot be null.");
                }
                _timeTracker = value;
            }
        }

        public async Task RenderAllReportsInParallelAsync()
        {
            var tasks = new List<Task>();

            foreach (var renderer in _renderers)
            {
                for (int i = 0; i < _numberOfRenderings; i++)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        var stopwatch = Stopwatch.StartNew();
                        var renderTime = await renderer.RenderAsync();
                        stopwatch.Stop();

                        _timeTracker.TrackTime(renderer.ReportPath, renderTime);
                        Console.WriteLine($"Rendered: {Path.GetFileName(renderer.ReportPath)}, Time: {renderTime}");
                    }));
                }
            }

            await Task.WhenAll(tasks);
        }

        public IReadOnlyDictionary<string, TimeSpan> GetLowestRenderTimes()
        {
            return _timeTracker.GetLowestRenderTimes();
        }
    }
}
