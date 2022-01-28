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
using System.IO;

namespace WPF_Tester_App
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        // this Form requires a dictionary 'the order' to be passed to it.
        public Basket(Dictionary<string, int> myOrder)
        {
            InitializeComponent();

            // display Order Info using the info gained from the dictionary compiled on the last Window.
            displayOrder(myOrder);
            setUpDetails(myOrder);
        }

        private Dictionary<string, int> fullOrder = new Dictionary<string, int> { };
        private Dictionary<string, double> prices = new Dictionary<string, double> { };

        private void displayOrder(Dictionary<string, int> myOrder)
        {
            prices = GetPrices();        // get prices from csv file.

            double Order_Total = 0.0;

            foreach (KeyValuePair<string, int> coffee in myOrder)
            {
                // Output the informaiton to the labels for each coffee on the order.
                lbl_Coffees.Content += string.Format("{0}\n", coffee.Key);
                lbl_Qty.Content += string.Format("{0}\n", coffee.Value);

                //---------------------------------
                double coffeePrice = 0.00;
                // get the coffee price from the second dictionary from the text file.

                if (!prices.TryGetValue(coffee.Key, out coffeePrice))
                {
                    // This bit happens IF the key IS NOT in the dictionary.
                    MessageBox.Show("Not Found");       // sensible output message
                }

                // output Price to label
                lbl_Price.Content += string.Format("£{0:0.00}\n", coffeePrice);

                //calculate and output subtotal for that coffee
                double subtotal = coffeePrice * coffee.Value;
                lbl_Subtotals.Content += string.Format("£{0:0.00}\n", subtotal);

                // add this subtotal to the big overall total.
                Order_Total += subtotal;
            }
            // Output the overall Order Total when the whole order has been completed.
            lbl_Order_Total.Content = string.Format("{0:0.00}\n", Order_Total);


        }

        private Dictionary<string, double> GetPrices()
        {
            Dictionary<string, double> priceList = new Dictionary<string, double> { };

            using (StreamReader reader = new StreamReader(@"TextFiles\Coffee_Price_List.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)      // while there is a line to be read, read it in.
                {
                    string[] splitLine = line.Split(',');       // split the line at the comma, separating (Name, Price)

                    priceList.Add(splitLine[0], double.Parse(splitLine[1]));        // Add to 'PriceList' Dictionary.
                }
            }

            return priceList;
        }

        private void setUpDetails(Dictionary<string, int> myOrder)
        {
            fullOrder = myOrder;
        }

        private void btn_CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();  // dummy use for Order ID.
            int orderID = rnd.Next(00001, 99999);

            double Order_Total = 0.0;

            string filename = string.Format("Order{0}.txt", orderID);
            string filepath = string.Format("TextFiles\\{0}", filename);

            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine(string.Format("Order ID: {0}", orderID));

                foreach (KeyValuePair<string, int> coffee in fullOrder)
                {
                    string coffeeOrdered = "";
                    // Output the informaiton to the labels for each coffee on the order.
                    coffeeOrdered += string.Format("{0},", coffee.Key);
                    coffeeOrdered += string.Format("{0},", coffee.Value);

                    //---------------------------------
                    double coffeePrice = 0.00;
                    // get the coffee price from the second dictionary from the text file.

                    if (!prices.TryGetValue(coffee.Key, out coffeePrice))
                    {
                        // This bit happens IF the key IS NOT in the dictionary.
                        MessageBox.Show("Not Found");       // sensible output message
                    }

                    //calculate and output subtotal for that coffee
                    double subtotal = coffeePrice * coffee.Value;
                    coffeeOrdered += string.Format("£{0:0.00}", subtotal);

                    // add this subtotal to the big overall total.
                    Order_Total += subtotal;

                    sw.WriteLine(coffeeOrdered);
                }
                sw.WriteLine(string.Format("Total: £{0}", Order_Total));
            }
        }
    }
}
