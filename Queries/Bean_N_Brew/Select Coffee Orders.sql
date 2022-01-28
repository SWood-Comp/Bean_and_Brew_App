SELECT * 		-- this brings back EVERYTHING, try SELECTing specific fields to come back
FROM coffee_orders, user_details
WHERE coffee_orders.custID = user_details.userID;   -- links the two tables together using IDs