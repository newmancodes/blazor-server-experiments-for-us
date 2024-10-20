using BlazorServer.Experiments.Web.Application;

namespace BlazorServer.Experiments.Web;

public interface ISearchSpecificationFormatter
{
    Task<string> FormatAsync(SearchSpecification searchSpecification);
}