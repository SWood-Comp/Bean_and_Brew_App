using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//-----------------------------------------
using MySql.Data.MySqlClient;
//-----------------------------------------

namespace WPF_Tester_App
{
    /// <summary>
    /// Interaction logic for BookTable.xaml
    /// </summary>
    public partial class BookTable : Window
    {
        public BookTable()
        {
            InitializeComponent();
        }

        private bool validateEntries()
        {
            bool nameOK, emailOK, dateOK;
            nameOK = emailOK = dateOK = true;  // set all flags to 'true'.

            string errorMsg = "";

            if (String.IsNullOrWhiteSpace(txt_EmailBox.Text))
            {
                txt_EmailBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCA, 0xCA));
                txt_EmailBox.Focus();
                nameOK = false;
                errorMsg += "Email needs to be filled out.";
            }
            if (String.IsNullOrWhiteSpace(txt_NameBox.Text))
            {
                txt_NameBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCA, 0xCA));
                txt_NameBox.Focus();
                emailOK = false;
                errorMsg += "Name needs to be filled out.";
            }

            if (dtp_DatePicker.SelectedDate < DateTime.Today)       // if date chosen is in the past. output error message
            {
                dtp_DatePicker.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCA, 0xCA));
                dtp_DatePicker.Focus();
                dateOK = false;
                errorMsg += "Date of Booking needed.";
            }

            if(nameOK == false || emailOK == false || dateOK == false)
            {
                MessageBox.Show(errorMsg);      // show the error message if any of the checks are failed.
                return false;
            }

            return true;
        }

        private void btn_BookTable_Click(object sender, RoutedEventArgs e)
        {
            bool validDetails = validateEntries();

            if (!validDetails)
            {
                // invalid detials used, no query executed.
                MessageBox.Show("Booking Unsuccessful, please check your information.");
            }
            else
            {
                bool existingBooking = checkBookingExists();  // returns True is booking already exists.
                bool seatsAvailable = checkAvailability();

                if (existingBooking == false && seatsAvailable == true)
                {
                    AddTableBooking();
                    MessageBox.Show("Booking Successful.");
                    string bookingInfo = string.Format("Booking for {0} people \non {1:d} @ {2} \nin {3}.",
                        sld_NumPeople.Value, dtp_DatePicker.SelectedDate, combo_Time.SelectedValue, combo_Location.SelectedValue);
                    // now show the final booking message.
                    MessageBox.Show(bookingInfo);
                }
                else
                {
                    // invalid detials used, no query executed.
                    MessageBox.Show("Unable to make Booking, please check date/time.");
                }
            }
        }

        //------------------------------------------------------------
        // SQL
        //------------------------------------------------------------
        public MySqlConnection ConnectToDB()
        {
            MySqlConnection conSQL = new MySqlConnection();
            // MySql Connection string for local MySql Server - the PC you are working on in ND college

            // use 'localhost' to pickup whichever machine you're on. (Wildcard)
            conSQL.ConnectionString = @"server=localhost; uid=root; database=bean_and_brew_db";

            return conSQL;
        }

        private void AddTableBooking()
        {
            MySqlConnection insertBooking = ConnectToDB();      // fetch the connection string

            //MessageBox.Show(combo_Time.SelectedValue.ToString());
            //MessageBox.Show(dtp_DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));

            // Structure of the SQL query which will be used to take data and INSERT INTO the database table specified.
            string CommandText = "INSERT INTO table_bookings(cust_name, cust_email, booking_date, booking_time, num_people, booking_store) " +
                "VALUES (@name, @email, @date, @time, @people, @store);";

            using (insertBooking)
            {
                MySqlCommand newTableCommand = new MySqlCommand(CommandText, insertBooking);

                newTableCommand.Parameters.Add("@name", MySqlDbType.VarChar);
                newTableCommand.Parameters["@name"].Value = txt_NameBox.Text;

                newTableCommand.Parameters.Add("@email", MySqlDbType.VarChar);
                newTableCommand.Parameters["@email"].Value = txt_EmailBox.Text;

                newTableCommand.Parameters.Add("@date", MySqlDbType.Date);
                newTableCommand.Parameters["@date"].Value = dtp_DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                // use the SelectedDate.Value and need to format it in such a way that the MySQL system will accept it.

                newTableCommand.Parameters.Add("@time", MySqlDbType.VarChar);
                newTableCommand.Parameters["@time"].Value = combo_Time.SelectedItem.ToString();

                newTableCommand.Parameters.Add("@people", MySqlDbType.Int32);
                newTableCommand.Parameters["@people"].Value = sld_NumPeople.Value;

                newTableCommand.Parameters.Add("@store", MySqlDbType.VarChar);
                newTableCommand.Parameters["@store"].Value = combo_Location.SelectedItem;

                try         // tries to run the query, any exceptions or issues will get caught below.
                {
                    insertBooking.Open();
                    newTableCommand.ExecuteReader();       // Here our query will be executed and data inserted into the database.

                    MessageBox.Show("New Table Booking made.");        // MessageBox used to visibly show the event has gone ahead.
                    insertBooking.Close();
                    // Close connection when done... keep things neat and tidy.
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (insertBooking == null)
                    {
                        MessageBox.Show("Connection Failed!");
                    }
                }

            }
        }

        private bool checkBookingExists()
        {
            bool exists = false;

            MySqlConnection bnb_Connect = ConnectToDB();      // fetch the connection string

            MySqlDataReader myDataReader;  // reads the data in from the database later.

            //---------------- SQL Query to find tables booking ------------------------------
            string findTableQuery = "SELECT count(cust_name) FROM table_bookings " +
                "WHERE booking_date = @date " +
                    "AND booking_time = @time " +
                    "AND cust_name = @name " +
                    "AND booking_store = @store;";
            //--------------------------------------------------------------------------------
            int matchesFound = -1;

            string dataSent = "";

            using (bnb_Connect)
            {
                MySqlCommand checkBooking = new MySqlCommand(findTableQuery, bnb_Connect);

                checkBooking.Parameters.Add("@name", MySqlDbType.VarChar);     // data type - VarChar
                checkBooking.Parameters["@name"].Value = txt_NameBox.Text;     // Value @name from TextBox

                checkBooking.Parameters.Add("@date", MySqlDbType.Date);     // data type - VarChar
                string formattedDate = dtp_DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                checkBooking.Parameters["@date"].Value = formattedDate;    
                // Value @date from DatePicker control, needs to be properly formatted to for MySQL

                checkBooking.Parameters.Add("@time", MySqlDbType.VarChar);     // data type - VarChar
                string time = combo_Time.SelectedItem.ToString();
                checkBooking.Parameters["@time"].Value = time;      // Value @time from a substring of time value

                checkBooking.Parameters.Add("@store", MySqlDbType.VarChar);     // data type - VarChar
                string store = combo_Location.SelectedItem.ToString();
                checkBooking.Parameters["@store"].Value = store;     // Value @date from DatePicker control

                dataSent = string.Format("Booking Check:\n\nName: {0}\nDate: {1}\nTime: {2}\nStore: {3}",txt_NameBox.Text, formattedDate, time, store);
                MessageBox.Show(dataSent);
                MessageBox.Show(findTableQuery);

                try         // tries to run the query, any exceptions/issues will get caught below.
                {
                    bnb_Connect.Open();
                    myDataReader = checkBooking.ExecuteReader();           // Execute the command using the DataReader

                    if (myDataReader.Read())        // if there is any data to read from this Query... THEN...
                    {
                        matchesFound = myDataReader.GetInt32(0);
                        // count only bring back one piece of data in only column
                    }

                    if (matchesFound > 0)
                    {
                        MessageBox.Show("Booking already exists!");        // MessageBox used to visibly show the event has gone ahead.
                        exists = true;
                    }

                    myDataReader.Close();
                    bnb_Connect.Close();            // Close connection when done... keep things neat and tidy.

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Booking Check:\nError Occurred:\n{0}",ex.Message));
                }
            }
            // end of SQL bit

            return exists;
        }

        private bool checkAvailability()
        {
            bool seatsLeft = false;

            MySqlConnection bnb_Connect = ConnectToDB();      // fetch the connection string

            MySqlDataReader myDataReader;  // reads the data in from the database later.

            //---------------- SQL Query to find tables booking ------------------------------
            string findSeatsQuery = "SELECT coalesce(sum(num_people),0) FROM table_bookings " +
                "WHERE booking_date = @date " +
                    "AND booking_store = @store " +
                    "AND booking_time = @time; ";

            //--------------------------------------------------------------------------------
            int seatsAvailable = -1;
            string dataSent = "";

            using (bnb_Connect)
            {
                MySqlCommand checkBooking = new MySqlCommand(findSeatsQuery, bnb_Connect);

                checkBooking.Parameters.Add("@date", MySqlDbType.Date);     // date - Date
                string formattedDate = dtp_DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                checkBooking.Parameters["@date"].Value = formattedDate;
                // Value @date from DatePicker control, needs to be properly formatted to for MySQL
                
                checkBooking.Parameters.Add("@store", MySqlDbType.VarChar);     // store - VarChar
                string store = combo_Location.SelectedItem.ToString();
                checkBooking.Parameters["@store"].Value = store;     // Value @store from ComboBox control
                
                checkBooking.Parameters.Add("@time", MySqlDbType.VarChar);     // data type - VarChar
                string time = combo_Time.SelectedItem.ToString();
                string timeHour = time.Substring(0, 3);                 // strips out just the "HH:" of the time slot
                checkBooking.Parameters["@time"].Value = time;      // Value @time from the time value above

                dataSent = string.Format("Availability Check\n\nDate: {0}\nTime: {1}\nStore: {2}", formattedDate, time, store);
                MessageBox.Show(dataSent);
                MessageBox.Show(findSeatsQuery);

                try         // tries to run the query, any exceptions/issues will get caught below.
                {
                    bnb_Connect.Open();
                    myDataReader = checkBooking.ExecuteReader();           // Execute the command using the DataReader

                    if (myDataReader.Read())        // if there is any data to read from this Query... THEN...
                    {
                        seatsAvailable = 40 - myDataReader.GetInt32(0);     // 40 seats in cafe
                    }

                    if (seatsAvailable < sld_NumPeople.Value)      // if seats left less than number wanted
                    {
                        string seatNum = string.Format("Seats Left at {0} store:  {1}", store, seatsAvailable);
                        MessageBox.Show(seatNum);      // show seats left.

                        string seatError = string.Format("Seating Check\nNot enough seats left at the {0} store!", store);
                        MessageBox.Show(seatError);      // MessageBox used to visibly show the error code.   

                        seatsLeft = false;
                    }
                    else
                    {
                        seatsLeft = true;
                    }

                    myDataReader.Close();
                    bnb_Connect.Close();            // Close connection when done... keep things neat and tidy.

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Seating Error Occurred:\n{0}", ex.Message));
                }
            }
            // end of SQL bit

            return seatsLeft;
        }

        //---------------------------------------------------------------------------------------------------------
        public MySqlConnection ConnectToHomeDB()
        {
            MySqlConnection conSQL = new MySqlConnection();

            //----------------------------------------------------------------------
            //MySQL Home setup - default password used...
            conSQL.ConnectionString = @"server=localhost; uid=root; database=bean_and_brew_db; password='SQLcomp'";

            //ALTER USER 'root'@'localhost' IDENTIFIED BY 'SQLcomp';     // SQL Command - to change the associated password with accounts
            //----------------------------------------------------------------------

            return conSQL;
        }
    }
}
