using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Employee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditEmployee : Page
    {
        public EditEmployee()
        {
            this.InitializeComponent();
            List<EmployeeLib.employeeDetail> hi = EmployeeLib.GetEmpInfo(EmployeesInfo.nama);
            foreach (var type in hi)
            {
                Names.Text = type.name;
                ID.Text = type.ID;
                DOB.Date = new DateTimeOffset(new DateTime(int.Parse(type.dob.Substring(6, 4)), int.Parse(type.dob.Substring(3, 2)), int.Parse(type.dob.Substring(0, 2))));
                number.Text = type.Number;
                nationality.Text = type.nationality;
                salary.Text = type.salary.ToString();
                

              
            }
        }


        //edit data

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {

            EmployeeLib.UpdateEmployee(EmployeesInfo.nama, Names.Text, ID.Text, DOB.Date.ToString().Substring(0, 10), number.Text, nationality.Text, salary.Text);
            Frame.Navigate(typeof(EmployeesInfo));
        }




    }
}