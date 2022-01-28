-- COALESCE returns a value or a substitute value given here as '0'

SELECT coalesce(sum(num_people), 0)		
FROM table_bookings
WHERE booking_date = "2021-11-26"
	AND booking_time LIKE "16%";		
    -- LIKE is able to search using values and wildcards '%'
	-- this matches patterns before/after/during a given string.