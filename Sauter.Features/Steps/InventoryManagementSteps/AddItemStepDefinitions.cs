﻿using FluentAssertions;
using Sauter.InventoryManagement;

namespace Sauter.Features.Steps.InventoryManagementSteps;

[Binding]
public sealed class AddItemStepDefinitions
{
    private string? _itemIdentifier;
    private string? _itemName;
    private int? _quantity;

    [When(@"Inputting an item with Name (.*) Quantity (.*)")]
    public void WhenInputtingAnItemWithNamePlasticCupQuantity(string itemName, int quantity)
    {
        _itemName = itemName;
        _quantity = quantity;
        _itemIdentifier = InventoryManagement.InventoryManagement.StoreItem(itemName, quantity);
    }
    
    [Then(@"The system records it")]
    public void ThenTheSystemRecordsIt()
    {
        ItemDto recordedItem =
            InventoryManagement.InventoryManagement.GetItem(_itemIdentifier!);
        
        recordedItem.Id.Should().Be(_itemIdentifier);
        recordedItem.Name.Should().Be(_itemName);
        recordedItem.Quantity.Should().Be(_quantity);
    }
}