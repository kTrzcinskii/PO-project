namespace FlightManager.NewsSource;
internal class NewsGenerator
{
    private List<INewsSource> newsSources;
    private List<IReportable> reportSubjects;
    private int SourceIndex { get; set; } = 0;
    private int SubjectIndex { get; set; } = 0;

    public NewsGenerator(List<INewsSource> sources, List<IReportable> subjects)
    {
        newsSources = sources;
        reportSubjects = subjects;
    }

    public string? GenerateNextNews()
    {
        if (SubjectIndex == reportSubjects.Count)
        {
            SubjectIndex = 0;
            SourceIndex++;
        }

        if (SourceIndex == newsSources.Count)
            return null;

        var source = newsSources[SourceIndex];
        var subject = reportSubjects[SubjectIndex];
        var report = subject.AcceptNewsSource(source);

        SubjectIndex++;

        return report;
    }

}
