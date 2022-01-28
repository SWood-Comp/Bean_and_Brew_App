SELECT sum(num_people) FROM table_bookings
WHERE booking_date = "2021-11-26"
	AND booking_time LIKE CONCAT("14:", '%')
    AND booking_store = "Harrogate";
    
SELECT sum(num_people) FROM table_bookings
WHERE booking_date = "2021-11-26"
	AND booking_time = "14:00"
    AND booking_store = "Harrogate";