namespace BlazorServer.Experiments.Web.Application;

public interface ISearchService
{
    Task<IEnumerable<SearchResult>> SearchAsync(SearchSpecification searchSpecification);
}