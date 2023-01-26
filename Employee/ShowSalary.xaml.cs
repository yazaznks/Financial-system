
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Employee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowSalary : Page
    {
        public ShowSalary()
        {
            this.InitializeComponent();
            userView.ItemsSource = EmployeeLib.GetAllSalaries();
        }

        private void s_TextChanged(object sender, TextChangedEventArgs e)
        {
            userView.ItemsSource = EmployeeLib.searchSalary(s.Text);
        }

        private void userView_AutoGeneratingColumn(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "SName")
            {
                e.Column.Header = "الموظف";
            }
            if (e.Column.Header.ToString() == "total")
            {
                e.Column.Header = "الراتب";
            }
            if (e.Column.Header.ToString() == "Month")
            {
                e.Column.Header = "الشهر";
            }
            if (e.Column.Header.ToString() == "absent")
            {
                e.Column.Header = "ايام الغياب";
            }
            if (e.Column.Header.ToString() == "notes")
            {
                e.Column.Header = "الملاحظات";
            }
        }

 

     
    }
}
