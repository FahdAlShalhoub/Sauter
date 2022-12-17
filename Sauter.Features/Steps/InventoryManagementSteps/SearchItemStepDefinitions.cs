using FluentAssertions;
using Sauter.InventoryManagement;

namespace Sauter.Features.Steps.InventoryManagementSteps;

[Binding]
public class SearchItemStepDefinitions
{
    private IEnumerable<ItemDto> _searchResults = null!;
    
    [When(@"Searching for (.*)")]
    public void WhenSearchingFor(string searchTerm)
    {
        _searchResults = InventoryManagementActions.SearchItemByName(searchTerm);
    }

    [Then(@"The result contain (.*)")]
    public void ThenTheResultShouldContain(string searchResult)
    {
        _searchResults.Should().HaveCount(2);
        _searchResults.Should().Contain(res => res.Name == searchResult);
    }
}