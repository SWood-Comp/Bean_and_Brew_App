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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Tester_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ////---------------- Scaling Window ------------------------------------
        //// https://stackoverflow.com/questions/3193339/tips-on-developing-resolution-independent-application/5000120#5000120

        //#region ScaleValue Dependency Property
        //public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        //private static object OnCoerceScaleValue(DependencyObject o, object value)
        //{
        //    MainWindow MainWindow = o as MainWindow;
        //    if (MainWindow != null)
        //        return MainWindow.OnCoerceScaleValue((double)value);
        //    else return value;
        //}

        //private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    MainWindow MainWindow = o as MainWindow;
        //    if (MainWindow != null)
        //        MainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        //}

        //protected virtual double OnCoerceScaleValue(double value)
        //{
        //    if (double.IsNaN(value))
        //        return 1.0f;

        //    value = Math.Max(0.1, value);
        //    return value;
        //}

        //protected virtual void OnScaleValueChanged(double oldValue, double newValue) { }

        //public double ScaleValue
        //{
        //    get => (double)GetValue(ScaleValueProperty);
        //    set => SetValue(ScaleValueProperty, value);
        //}
        //#endregion

        //private void MainGrid_SizeChanged(object sender, EventArgs e) => CalculateScale();

        //private void CalculateScale()
        //{
        //    double yScale = ActualHeight / 854f;        // uses original x and y values
        //    double xScale = ActualWidth / 490f;
        //    double value = Math.Min(xScale, yScale);

        //    ScaleValue = (double)OnCoerceScaleValue(formHome, value);
        //}

        //---------------------------------------------------------------------------------------------

        private void btn_BookTable_Click(object sender, RoutedEventArgs e)
        {
            BookTable newTableBooking = new BookTable();
            newTableBooking.Closed += OtherForm_Closed;         // when the 'newBookingTable' form is closed, run the below subroutine to then'Show' this back up on screen.
            this.Hide();                                        // in the meantime 'Hide' this Main Menu Form.
            newTableBooking.Show();
        }

        private void OtherForm_Closed(object sender, EventArgs e)
        {

            Show();     // When the other form is closed, call this subroutine and Show this back to screen.
        }



        private void btn_PreorderCoffee_Click(object sender, RoutedEventArgs e)
        {
            PreOrder_Coffee newCoffeeOrder = new PreOrder_Coffee();
            newCoffeeOrder.Closed += OtherForm_Closed;          // when the 'New Form' is closed, run the below subroutine to then'Show' this back up on screen.
            this.Hide();                                        // in the meantime 'Hide' this Main Menu Form.
            newCoffeeOrder.Show();
        }

        private void btn_PreorderBakedGoods_Click(object sender, RoutedEventArgs e)
        {
            PreOrder_BakedGoods newBakedOrder = new PreOrder_BakedGoods();
            newBakedOrder.Closed += OtherForm_Closed;          // when the 'New Form' is closed, run the below subroutine to then'Show' this back up on screen.
            this.Hide();                                        // in the meantime 'Hide' this Main Menu Form.
            newBakedOrder.Show();
        }
    }
}
