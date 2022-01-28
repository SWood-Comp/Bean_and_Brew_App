SELECT count(cust_name) FROM table_bookings
WHERE booking_date = "2021-11-26"
	AND booking_time = "14:00"
    AND booking_store = "Harrogate"
    AND cust_name = "StephyG";
    
SELECT cust_name, booking_date, booking_time, booking_store FROM table_bookings
WHERE booking_date = "2021-11-26"
	AND booking_time = "14:00"
    AND booking_store = "Harrogate"
    AND cust_name = "StephyG";