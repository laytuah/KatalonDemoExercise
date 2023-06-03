Feature: ViewCart

	#Pre-requisite: Four random Items must be added to cart
Scenario: 01_Verify that the cart is left with three items when one with lowest price is removed.
	Given that katalon Ecommerce website is loaded
	When a user adds 4 items to cart
	And a user clicks on view cart
	Then a user is able to get a total of 4 items
	When a user clicks on the remove item button for the lowest price item
	Then a user is able to get a total of 3 items