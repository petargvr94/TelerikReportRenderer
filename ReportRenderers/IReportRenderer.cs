namespace TelerikReportRenderer.ReportRenderers
{
    public interface IReportRenderer
    {
        string ReportPath { get; }
        Task<TimeSpan> RenderAsync();
    }
}
