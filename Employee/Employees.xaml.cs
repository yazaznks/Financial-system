using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Employee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Employees : Page
    {
        public Employees()
        {
            this.InitializeComponent();
        }

        private void newE(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(addEmployee));
        }

        private void deta(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeesInfo));
        }

        private void salary(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(salaryC));
        }

        private void SalaryC_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowSalary));
        }

        private void Salaryprint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
