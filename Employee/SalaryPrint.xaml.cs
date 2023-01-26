using Microsoft.Toolkit.Uwp.Helpers;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Employee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SalaryPrint : Page
    {
        public SalaryPrint()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<EmployeeLib.employeeDetail> hi = EmployeeLib.GetEmpInfo(EmployeesInfo.nama);
            foreach (var type in hi)
            {

                student.Text = type.name;
                id.Text = type.ID;
                date.Text = type.dob;
                dateS.Text = EmployeeLib.GetAllSalaries().Last().Month;
                sal.Text = EmployeeLib.GetAllSalaries().Last().total.ToString();
                notes.Text = EmployeeLib.GetAllSalaries().Last().notes.ToString();
                var printHelper = new PrintHelper(print);
                printHelper.ShowPrintUIAsync("Nedaa app", true);
            }
        }
    }
}
