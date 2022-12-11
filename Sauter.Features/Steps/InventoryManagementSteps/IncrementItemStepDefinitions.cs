using FluentAssertions;
using Sauter.InventoryManagement;

namespace Sauter.Features.Steps.InventoryManagementSteps;

[Binding]
public sealed class IncrementItemStepDefinitions
{
    private string? _itemIdentifier;
    private int amount;
    
    [Given(@"An item with Name (.*) and Quantity (.*)")]
    public void GivenAnItemWithNameAndQuantity(string name, int quantity)
    {
        _itemIdentifier = InventoryManagement.InventoryManagement.StoreItem(name, quantity);
    }
    
    [When(@"Item is incremented by (.*)")]
    public void WhenIncremented(int incrementAmount)
    {
        amount = incrementAmount;
        InventoryManagement.InventoryManagement.IncrementItem(_itemIdentifier!, incrementAmount);
    }
    
    [When(@"Item is decremented by (.*)")]
    public void WhenItemIsDecrementedBy(int decrementAmount)
    {
        amount = decrementAmount;
        InventoryManagement.InventoryManagement.DecrementItem(_itemIdentifier!, decrementAmount);
    }

    [Then(@"The quantity of the item equals (.*)")]
    public void ThenTheQuantityOfTheItemIncreasesByOne(int result)
    {
        ItemDto item = InventoryManagement.InventoryManagement.GetItemById(_itemIdentifier!);

        item.Quantity.Should().Be(result);
    }
    
    [Then(@"IncreaseItem action should be logged")]
    public void ThenIncreaseItemShouldBeLogged()
    {
        InventoryManagement.InventoryManagement.GetAuditLogs()
            .Should()
            .ContainSingle(log => log.Action == $"Action IncreaseItem Was Committed With Input {_itemIdentifier} - {amount}");
    }
    
    [Then(@"DecreaseItem action should be logged")]
    public void ThenDecreaseItemShouldBeLogged()
    {
        InventoryManagement.InventoryManagement.GetAuditLogs()
            .Should()
            .ContainSingle(log => log.Action == $"Action DecreaseItem Was Committed With Input {_itemIdentifier} - {amount}");
    }
}