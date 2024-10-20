namespace BlazorServer.Experiments.Web.Application;

public record SearchSpecification(
    string SearchTerm,
    Pagination Pagination,
    SortSpecification SortSpecification)
{
    internal static SearchSpecification Default => new SearchSpecification(
        string.Empty,
        Pagination.Default,
        SortSpecification.Default);
}

public record Pagination(
    byte PageSize,
    ulong PageNumber)
{
    internal static Pagination Default => new Pagination(
        10,
        1);
}

public record SortSpecification(
    string SortBy,
    SortDirection Direction)
{
    internal static SortSpecification Default => new SortSpecification(
        Sortables.Relevancy,
        SortDirection.Descending);
}

public enum SortDirection
{
    Ascending = 0,
    Descending = 1
}

public static class Sortables
{
    public static string Relevancy = nameof(Relevancy);
}