using FluentAssertions;
using Sauter.AuditLog;
using Sauter.InventoryManagement;

namespace Sauter.Features.Steps.InventoryManagementSteps;

[Binding]
public sealed class IncrementItemStepDefinitions
{
    private string? _itemIdentifier;
    private int _amount;
    
    [Given(@"An item with Name (.*) and Quantity (.*)")]
    public void GivenAnItemWithNameAndQuantity(string name, int quantity)
    {
        _itemIdentifier = InventoryManagementActions.StoreItem(name, quantity);
    }
    
    [When(@"Item is incremented by (.*)")]
    public void WhenIncremented(int incrementAmount)
    {
        _amount = incrementAmount;
        InventoryManagementActions.IncrementItem(_itemIdentifier!, incrementAmount);
    }
    
    [When(@"Item is decremented by (.*)")]
    public void WhenItemIsDecrementedBy(int decrementAmount)
    {
        _amount = decrementAmount;
        InventoryManagementActions.DecrementItem(_itemIdentifier!, decrementAmount);
    }

    [Then(@"The quantity of the item equals (.*)")]
    public void ThenTheQuantityOfTheItemIncreasesByOne(int result)
    {
        ItemDto item = InventoryManagementActions.GetItemById(_itemIdentifier!);

        item.Quantity.Should().Be(result);
    }
    
    [Then(@"(.*) action should be logged")]
    public void ThenIncreaseItemShouldBeLogged(string actionName)
    {
        AuditLogActions.GetAuditLogs()
            .Should()
            .ContainSingle(log => log.Action == $"Action {actionName} Was Committed With Input {_itemIdentifier} - {_amount}");
    }
}