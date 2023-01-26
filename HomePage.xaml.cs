using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            if (Login.permission == "1")
            {
                border2.Visibility = Visibility.Collapsed;
                finance.Visibility = Visibility.Collapsed;
                employee.Visibility = Visibility.Collapsed;
            }

        }

        private void btn1_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(studentInfo));
        }

        private void btn2_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddStudent));
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Debit));
        }

        private void finance_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Finance));
        }

        private void employee_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Employee.Employees));
        }
    }
}
