SELECT coffee_orders.orderID, orderDate, store, Username, CoffeeName, QuantityOrdered
FROM user_details, coffee_orders, coffee_orderdetails, coffee_products		-- selected data comes from multiple tables
WHERE user_details.userID = coffee_orders.custID						-- links user_details and coffee_orders
	AND coffee_orders.orderID = coffee_orderdetails.orderID				-- links coffee_orders and coffee_orderdetails
	AND coffee_orderdetails.coffeeOrdered = coffee_products.coffeeID	-- links coffee_products and coffee_orderdetails
    -- Now add Criteria for the Query here...
    AND store = "Leeds"
ORDER BY orderDate;

-- Too much data comes back though, so we need to be more specific in the SELECT statement