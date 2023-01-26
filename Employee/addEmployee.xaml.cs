using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class addEmployee : Page
    {
        public addEmployee()
        {
            this.InitializeComponent();
        }

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            EmployeeLib.addEmployee(Names.Text, DOB.Date.ToString().Substring(0, 10), ID.Text, num.Text, nationality.Text, salary.Text);
            Frame.Navigate(typeof(HomePage));
        }
    }
}
