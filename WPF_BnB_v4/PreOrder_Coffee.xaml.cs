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

namespace WPF_Tester_App
{
    /// <summary>
    /// Interaction logic for PreOrder_Coffee.xaml
    /// </summary>
    public partial class PreOrder_Coffee : Window
    {
        public PreOrder_Coffee()
        {
            InitializeComponent();
        }

        public struct Coffee       // Template Data Structure for a 'Coffee' object in the system.
        {
            public string CoffeeName;
            public double CoffeePrice;
            public int QuantityOrdered;
            public bool Caffeinated;

            public Coffee(string name, double price, int qty, bool caff)
            {
                CoffeeName = name;
                CoffeePrice = price;
                QuantityOrdered = qty;
                Caffeinated = caff;
            }
        }

        private void btn_PreOrderCoffee_Click(object sender, RoutedEventArgs e)
        {
            Basket OrderBasket = new Basket(CoffeeOrder);

            OrderBasket.Show();            // show basket
        }

        Dictionary<string, int> CoffeeOrder = new Dictionary<string, int> { };   // blank dictionary for order of coffee.

        private void AddCoffee(object sender, RoutedEventArgs e)
        {
            // The below line enables mulitple Button objects to make use of this subroutine.
            // When any Button calls this, it allows the subroutine access to its attributes.
            Button selectedCoffee = sender as Button;

            string chosen = selectedCoffee.Tag.ToString();      // get the chosen coffee from the Button.Tag

            if (CoffeeOrder.ContainsKey(chosen))     // if found in Dictionary already...
            {
                CoffeeOrder[chosen]++;          // increase the value by 1 for that Key.
            }
            else
            {
                CoffeeOrder.Add(chosen, 1);     // if not found, add a new entry (Key, Value)
            }

            string ordered = string.Format("{0} ordered", chosen);
            MessageBox.Show(ordered);
        }

        List<Coffee> coffees_ordered = new List<Coffee> { };        // List of 'Coffee' data structures

        private void AddCoffee_New(object sender, RoutedEventArgs e)
        {
            // The below line enables mulitple Button objects to make use of this subroutine.
            // When any Button calls this, it allows the subroutine access to its attributes.
            Button selectedCoffee = sender as Button;

            string chosen = selectedCoffee.Tag.ToString();      // get the chosen coffee from the Button.Tag

            if (CoffeeOrder.ContainsKey(chosen))     // if found in Dictionary already...
            {
                CoffeeOrder[chosen]++;          // increase the value by 1 for that Key.
            }
            else
            {
                CoffeeOrder.Add(chosen, 1);     // if not found, add a new entry (Key, Value)
            }

            string ordered = string.Format("{0} ordered", chosen);
            MessageBox.Show(ordered);
        }
    }
}
