using Bogus;

namespace BlazorServer.Experiments.Web.Application;

public class RandomNonsenseSearchService : ISearchService
{
    private readonly Faker<SearchResult> _searchResultFaker;

    public RandomNonsenseSearchService()
    {
        _searchResultFaker = new Faker<SearchResult>()
            .RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.Title, _ => _.Lorem.Sentence());
    }
    
    public async Task<IEnumerable<SearchResult>> SearchAsync(SearchSpecification searchSpecification)
    {
        // Simulate the search timing.
        await Task.Delay(150);

        return _searchResultFaker.Generate(searchSpecification.Pagination.PageSize);
    }
}