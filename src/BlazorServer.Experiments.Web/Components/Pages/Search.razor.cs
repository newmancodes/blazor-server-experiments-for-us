using BlazorServer.Experiments.Web.Application;
using Microsoft.AspNetCore.Components;

namespace BlazorServer.Experiments.Web.Components.Pages;

public partial class Search
{
    [Inject]
    public ISearchSpecificationParser SpecificationParser { get; set; }

    [Inject]
    private ISearchService SearchService { get; set; }

    private SearchSpecification SearchSpecification { get; set; } = SearchSpecification.Default;

    [Parameter]
    public string? RawSpecification { get; set; }

    private bool IsLoading { get; set; } = true;
    
    private IEnumerable<SearchResult> Results { get; set; } = Array.Empty<SearchResult>();
    
    protected override async Task OnParametersSetAsync()
    {
        IsLoading = true;
        SearchSpecification = await SpecificationParser.ParseAsync(RawSpecification ?? string.Empty) switch
        {
            { IsValid: true } result => result.SearchSpecification,
            _ => SearchSpecification.Default
        };

        Results = await SearchService.SearchAsync(SearchSpecification);
        IsLoading = false;
    }
}