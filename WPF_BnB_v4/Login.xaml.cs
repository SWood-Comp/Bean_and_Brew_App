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
//--------------------------------------
using MySql.Data.MySqlClient;

namespace WPF_Tester_App
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OtherForm_Closed(object sender, EventArgs e)
        {
            Show();     // When the other form is closed, call this subroutine and Show this back to screen.
        }

        private void btn_Create_Account_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createUser = new CreateAccount();
            createUser.Closed += OtherForm_Closed;         // when the 'createUser' form is closed, run the below subroutine to then'Show' this back up on screen.
            this.Hide();                                   // in the meantime 'Hide' this Login Form.
            createUser.Show();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            bool match = FetchUserDetails();

            if (match)
            {
                MainWindow mainMenu = new MainWindow();
                mainMenu.Closed += OtherForm_Closed;         // when the 'mainMenu' form is closed, run the below subroutine to then'Show' this back up on screen.
                this.Hide();                                 // in the meantime 'Hide' this Login Form.
                mainMenu.Show();
            }
            else
            {
                MessageBox.Show("No match found, please try again.\nOr sign up for an account if you are new here.");
            }
        }

        private bool FetchUserDetails()
        {
            bool matchFound = false;

            MySqlConnection bnb_Connect = ConnectToDB();      // fetch the connection string

            MySqlDataReader myDataReader;  // reads the data in from the database later.

            //string CommandText = "SELECT Username, Password FROM user_details" +
            //    "WHERE (Username='@username' OR Email='@email') AND Password='@passw'";

            //---------------------------- TEST ----------------------------------------------
            string CommandText = "SELECT Username, Email, Password " +
                "FROM user_details " +
                "WHERE Username=@username AND Password=@passw";
            //--------------------------------------------------------------------------------

            using (bnb_Connect)
            {
                MySqlCommand findUser = new MySqlCommand(CommandText, bnb_Connect);

                findUser.Parameters.Add("@username", MySqlDbType.VarChar);      // give the data type expected
                findUser.Parameters["@username"].Value = txt_Username.Text;     // give the value for this placeholder @.......
                // adding variables by parameters is a safer way to handle these things.

                findUser.Parameters.Add("@passw", MySqlDbType.VarChar);
                findUser.Parameters["@passw"].Value = txt_PasswordBox.Password; // doesn't have a 'Text' attribute

                try         // tries to run the query, any exceptions or issues will get caught below.
                {
                    bnb_Connect.Open();
                    MessageBox.Show("Executing SQL.");
                    myDataReader = findUser.ExecuteReader();           // Execute the command using the DataReader

                    MessageBox.Show("Finished SQL execution.");
                    if (myDataReader.Read())        // if there is any data to read from this Query... THEN...
                    {
                        MessageBox.Show("Connection made. \nUser Match Found.");        // MessageBox used to visibly show the event has gone ahead.
                        matchFound = true;

                        string name = myDataReader.GetString(0);   // the 1 implies the column with an index of 1
                        string email = myDataReader["Email"].ToString();    // another way of accessing attributes/columns ... by name.
                        string pass = myDataReader.GetString(2);    // this system counts from 0 as usual (on the resulting table

                        MessageBox.Show(string.Format("name: {0}\npass: {1}\nemail: {2}", name, pass, email));
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

            return matchFound;
        }

        public MySqlConnection ConnectToDB()
        {
            MySqlConnection conSQL = new MySqlConnection();
            // MySql Connection string for local MySql Server - the PC you are working on in ND college

            // use 'localhost' to pickup whichever machine you're on. (Wildcard)
            conSQL.ConnectionString = @"server=localhost; uid=root; database=bean_and_brew_db";

            return conSQL;
        }

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
