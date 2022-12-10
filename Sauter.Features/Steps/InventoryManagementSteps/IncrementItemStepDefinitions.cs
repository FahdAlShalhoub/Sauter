using FluentAssertions;
using Sauter.InventoryManagement;

namespace Sauter.Features.Steps.InventoryManagementSteps;

[Binding]
public sealed class IncrementItemStepDefinitions
{
    private string? _itemIdentifier;
    
    [Given(@"An item with Name (.*) and Quantity (.*)")]
    public void GivenAnItemWithNameAndQuantity(string name, int quantity)
    {
        _itemIdentifier = InventoryManagement.InventoryManagement.StoreItem(name, quantity);
    }
    
    [When(@"Item is incremented by (.*)")]
    public void WhenIncremented(int incrementAmount)
    {
        InventoryManagement.InventoryManagement.IncrementItem(_itemIdentifier!, incrementAmount);
    }
    
    [When(@"Item is decremented by (.*)")]
    public void WhenItemIsDecrementedBy(int decrementAmount)
    {
        InventoryManagement.InventoryManagement.DecrementItem(_itemIdentifier!, decrementAmount);
    }

    [Then(@"The quantity of the item equals (.*)")]
    public void ThenTheQuantityOfTheItemIncreasesByOne(int result)
    {
        ItemDto item = InventoryManagement.InventoryManagement.GetItem(_itemIdentifier!);

        item.Quantity.Should().Be(result);
    }
}