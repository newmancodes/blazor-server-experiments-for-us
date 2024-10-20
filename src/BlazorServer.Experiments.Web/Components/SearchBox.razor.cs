using BlazorServer.Experiments.Web.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorServer.Experiments.Web.Components;

public partial class SearchBox
{
    [Inject]
    public ISearchSpecificationFormatter SearchSpecificationFormatter { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [Parameter]
    public SearchSpecification Specification { get; set; }

    public string SearchTerm { get; set; }

    protected override void OnInitialized()
    {
        SearchTerm = Specification.SearchTerm;
    }

    private async Task PerformSearch(MouseEventArgs arg)
    {
        var targetSpecification = Specification with { SearchTerm = SearchTerm };
        var slug = await SearchSpecificationFormatter.FormatAsync(targetSpecification);
        NavigationManager.NavigateTo($"/search/{slug}");
    }
}