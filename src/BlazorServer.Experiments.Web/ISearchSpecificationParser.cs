using System.Diagnostics.CodeAnalysis;
using BlazorServer.Experiments.Web.Application;

namespace BlazorServer.Experiments.Web;

public interface ISearchSpecificationParser
{
    Task<SearchSpecificationParserResult> ParseAsync(string s);
}

public readonly struct SearchSpecificationParserResult
{
    [MemberNotNullWhen(true, nameof(Application.SearchSpecification))]
    public bool IsValid => SearchSpecification is not null;

    public SearchSpecification? SearchSpecification { get; init; }
}