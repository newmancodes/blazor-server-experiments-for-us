namespace BlazorServer.Experiments.Web.Application;

public class PredictableNonsenseSearchService : ISearchService
{
    public async Task<IEnumerable<SearchResult>> SearchAsync(SearchSpecification searchSpecification)
    {
        // Simulate the search timing.
        await Task.Delay(150);

        var itemRange = Enumerable.Range(((int)searchSpecification.Pagination.PageNumber - 1) * searchSpecification.Pagination.PageSize, searchSpecification.Pagination.PageSize);
        var searchResults = new List<SearchResult>();
        foreach (var item in itemRange)
        {
            var searchResult = new SearchResult
            {
                Id = Guid.Empty,
                Title = $"{searchSpecification.SearchTerm} item {item}"
            };
            searchResults.Add(searchResult);
        }

        return searchResults;
    }
}