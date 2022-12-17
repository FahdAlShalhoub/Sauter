Feature: Inventory Management
A comprehensive Inventory Management Sub-Module for managing physical assets within the enterprise. 

Scenario: Add New Item To Inventory 
	When Inputting an item with Name Plastic Cup Quantity 2 
	Then The system records it
	
Scenario: Increase Item Quantity In Inventory
	Given An item with Name Plastic Cup and Quantity 1 
	When Item is incremented by 4
	Then The quantity of the item equals 5
	
Scenario: Decrease Item Quantity In Inventory
	Given An item with Name Plastic Cup and Quantity 10 
	When Item is decremented by 4
	Then The quantity of the item equals 6
	
Scenario: Search Item By Name
	Given An item with Name Plastic Cup and Quantity 10
	And An item with Name Paper Cup and Quantity 1 
	And An item with Name Plastic Straw and Quantity 1 
	When Searching for Cup
	Then The result contain Paper Cup
	And The result contain Plastic Cup

@StoreItem	
Scenario: Record StoreItem Audit Log
	When Inputting an item with Name Plastic Cup Quantity 2 
	Then StoreItem action should be logged
	
Scenario: Record IncreaseItem AuditLog 
	Given An item with Name Plastic Cup and Quantity 1 
	When Item is incremented by 4
	Then IncrementItem action should be logged

Scenario: Record DecreaseItem AuditLog 
	Given An item with Name Plastic Cup and Quantity 1 
	When Item is decremented by 4
	Then DecrementItem action should be logged