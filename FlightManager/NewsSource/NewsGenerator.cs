namespace FlightManager.NewsSource;
internal class NewsGenerator
{
    private List<INewsSource> newsSources;
    private List<IReportable> reportSubjects;

    public NewsGenerator(List<INewsSource> sources, List<IReportable> subjects)
    {
        newsSources = sources;
        reportSubjects = subjects;
    }
}
