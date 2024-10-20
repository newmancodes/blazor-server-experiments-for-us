using BlazorServer.Experiments.Web.Application;
using FluentAssertions;

namespace BlazorServer.Experiments.Web.Tests;

public class SearchSpecificationUrlMarshallerTests
{
    [Fact]
    public async Task Search_Specification_Can_Be_Marshalled()
    {
        // Arrange
        var searchSpecification = new SearchSpecification(
            "some_search_term",
            new Pagination(25, 3),
            new SortSpecification("some_sort_by", SortDirection.Ascending)
        );
        var sut = new SearchSpecificationUrlMarshaller();
        
        var formatted = await sut.FormatAsync(searchSpecification);

        // Act
        var parserResult = await sut.ParseAsync(formatted);

        // Assert
        parserResult.IsValid.Should().BeTrue();
        parserResult.SearchSpecification.Should().Be(searchSpecification);
        parserResult.Should().NotBeSameAs(searchSpecification);
    }
}