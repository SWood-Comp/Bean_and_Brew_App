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
//---------------------------------------
using MySql.Data.MySqlClient;       // adds MySQL features.
//---------------------------------------

namespace WPF_Tester_App
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btn_Create_Account_Click(object sender, RoutedEventArgs e)
        {
            bool emailMatch = false;
            bool passwMatch = false;

            if (txt_Email.Text == txt_Email_Confirm.Text)
            {
                emailMatch = true;
            }

            string passw = txt_PasswordBox.Password;
            if (passw.Length > 6 && passw.Length < 25)
            {
                passwMatch = true;
            }

            if (passwMatch && emailMatch)
            {
                bool exists = checkNewUser();
                MessageBox.Show(exists.ToString());
                if (exists)
                {
                    MessageBox.Show("Username already exists please try another.");
                }
                else
                {
                    InsertNewUser();        // only enter details when validated
                }
            }
        }

        public MySqlConnection ConnectToDB()
        {
            MySqlConnection conSQL = new MySqlConnection();
            // MySql Connection string for local MySql Server - the PC you are working on in ND college

            // use 'localhost' to pickup whichever machine you're on. (Wildcard)
            conSQL.ConnectionString = @"server=localhost; uid=root; database=bean_and_brew_db";

            MessageBox.Show("Connection has been made successfully.");

            return conSQL;
        }

        private void InsertNewUser()
        {
            MySqlConnection insertSQL = ConnectToDB();      // fetch the connection string

            // Structure of the SQL query which will be used to take data and INSERT INTO the database table specified.
            string CommandText = "INSERT INTO user_details(Username, Email, Password) " +
                "VALUES (@username, @email, @passw);";

            using (insertSQL)
            {
                MySqlCommand insertDataCommand = new MySqlCommand(CommandText, insertSQL);

                insertDataCommand.Parameters.Add("@username", MySqlDbType.VarChar);
                insertDataCommand.Parameters["@username"].Value = txt_Username.Text;
                // ideally you should validate and check the data you insert into your DB.
                // adding variables by parameters is a safer way to handle these things.

                insertDataCommand.Parameters.Add("@email", MySqlDbType.VarChar);
                insertDataCommand.Parameters["@email"].Value = txt_Email.Text;

                insertDataCommand.Parameters.Add("@passw", MySqlDbType.VarChar);
                insertDataCommand.Parameters["@passw"].Value = txt_PasswordBox.Password;  // use the .Password attribute instead of Text.

                // Another way of running the above is on the line below...
                // this simply does the same but leaves out the SQL DB Data Type part, 
                // so it isn't as explicit, but it would still work.

                // insertDataCommand.Parameters.AddWithValue("@username", txt_Username.Text);

                try         // tries to run the query, any exceptions or issues will get caught below.
                {
                    insertSQL.Open();

                    insertDataCommand.ExecuteReader();       // Here our query will be executed and data inserted into the database.

                    MessageBox.Show("Connection made. \nInserting New User Data...");        // MessageBox used to visibly show the event has gone ahead.

                    insertSQL.Close();
                    // Close connection when done... keep things neat and tidy.
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (insertSQL == null)
                    {
                        MessageBox.Show("Connection Failed!");
                    }
                }

            }
        }

        private bool checkNewUser()
        {
            bool exists = false;

            MySqlConnection bnb_Connect = ConnectToDB();      // fetch the connection string

            MySqlDataReader myDataReader;  // reads the data in from the database later.

            //---------------------------- TEST ----------------------------------------------
            string CommandText = "SELECT count(Username) FROM user_details WHERE Username=@username";
            //--------------------------------------------------------------------------------
            int matchesFound = -1;

            using (bnb_Connect)
            {
                MySqlCommand checkUser = new MySqlCommand(CommandText, bnb_Connect);

                checkUser.Parameters.Add("@username", MySqlDbType.VarChar);      // give the data type expected
                checkUser.Parameters["@username"].Value = txt_Username.Text;     // give the value for this placeholder @.......

                try         // tries to run the query, any exceptions/issues will get caught below.
                {
                    bnb_Connect.Open();
                    MessageBox.Show("Executing SQL.");
                    myDataReader = checkUser.ExecuteReader();           // Execute the command using the DataReader

                    MessageBox.Show("Finished SQL execution.");
                    if (myDataReader.Read())        // if there is any data to read from this Query... THEN...
                    {
                        matchesFound = myDataReader.GetInt32(0);
                        // count only bring back one piece of data in only column
                    }

                    if (matchesFound > 0)
                    {
                        MessageBox.Show("User already exists!");        // MessageBox used to visibly show the event has gone ahead.
                        exists = true;
                    }

                    myDataReader.Close();
                    bnb_Connect.Close();            // Close connection when done... keep things neat and tidy.

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            // end of SQL bit

            return exists;
        }
    }
}
