
---------------------------------------------------------------
Coding Exercise - CrocerCo Shopping Cart
Author: Kevin Purpur
Date: 2016/01/11
---------------------------------------------------------------

---------------------------
Architecture
---------------------------
- "localdb" is used as the database
- EntityFramework is used to access the DB
- Application is setup in layers, that allow for a simple separation of code, that can in turn
  be enhanced to handle an "N-Tier" scenario
- Interfaces are used for Testability (ex. mocking)

---------------------------
Setup
---------------------------
DB Setup:
	Uses "(localdb)\v11.0""
	Option 1: Attach the DB from the backup location ("C:\Projects\Blatant\GroceryCo\Database\Backup")
	Option 2:
		- Edit the script \Blatant\GroceryCo\Database\FullDB.sql to set the following to the desired location:
			C:\Projects\Blatant\GroceryCo\Database\GroceryCo.mdf (set desired)
			C:\Projects\Blatant\GroceryCo\Database\GroceryCo_log.ldf (set desired)
		- Run the script \Blatant\GroceryCo\Database\FullDB.sql

	Note: the folders "Tables" and "Lookups" include the original DB files, but do not need to be run for option 1 or 2

---------------------------
Running the Application
---------------------------

- Set "GroceryCo.ConsoleApp" as the startup app
- Choose "C" to process the shopping cart
	- "ShoppingCart.txt" is the source file.  Product codes must match the "Product" table
- Choose "L" to list all products for maintenance.
	- Table "Products" handles the product list
	- Table "Promotion" handles the configuration of the promotions
		- "PromotionCode" of "p" is price based
			- "Amount" is the price that is applied to a "Quantity" grouping.  "ApplyTo" column is not used (should be null)
			Ex 1. Buy 3 at $2: 
				  PromotionCode: p
				  Quantity: 3
				  ApplyTo: null (not used for PromotionCode "p")
				  Amount: 2
		- "PromotionCode" of "d" is discount based
			- "Amount" is the discount to rate (0 = 100% off, .5 = 50% off) that is applied once a "Quantity" of items
			  have been purchased.  The "ApplyTo" sets the number of items that will be discounted after the original
			  "Quantity" has been purchased.
			Ex 1. Buy 1 get next item free: 
				  PromotionCode: d
				  Quantity: 1
				  ApplyTo: 1
				  Amount: 0
			Ex 2. Buy 1 get next item 50% off: 
				  PromotionCode: d
				  Quantity: 1
				  ApplyTo: 1
				  Amount: 0.5
			Ex 3. Buy 2 get next 3 items 50% off: 
				  PromotionCode: d
				  Quantity: 2
				  ApplyTo: 3
				  Amount: 0.5