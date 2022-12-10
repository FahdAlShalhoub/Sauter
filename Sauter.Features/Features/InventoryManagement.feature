Feature: Inventory Management
A comprehensive Inventory Management Sub-Module for managing physical assets within the enterprise. 

@mytag
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