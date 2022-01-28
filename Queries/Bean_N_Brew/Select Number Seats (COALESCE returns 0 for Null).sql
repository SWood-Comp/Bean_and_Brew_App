-- the COALESCE function, will return a value specified instead of 'null'
-- or it will return the value if it finds one to calculate

SELECT coalesce(sum(num_people),0) FROM table_bookings
WHERE booking_date = "2021-11-29"
	AND booking_store = "Leeds"
    AND booking_time = "12:00";