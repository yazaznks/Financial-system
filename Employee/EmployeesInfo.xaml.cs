using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeesInfo : Page
    {
        public EmployeesInfo()
        {
            this.InitializeComponent();
            empView.ItemsSource = EmployeeLib.GetEmployees();
        }


        private void empView_AutoGeneratingColumn(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
        
                if (e.Column.Header.ToString() == "name")
                {
                    e.Column.Header = "الإسم";
                }
                if (e.Column.Header.ToString() == "dob")
                {
                    e.Column.Header = "تاريخ الميلاد";
                }
                if (e.Column.Header.ToString() == "ID")
                {
                    e.Column.Header = "الرقم الشخصي";
                }
                if (e.Column.Header.ToString() == "Number")
                {
                    e.Column.Header = "رقم الجوال";
               
                }
                if (e.Column.Header.ToString() == "nationality")
                {
                    e.Column.Header = "الجنسية";
                }
                if (e.Column.Header.ToString() == "salary")
                {
                    e.Column.Header = "الراتب";
                }
             

            }

        public static int nama;
      
        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }
        private void btn3_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            EmployeeLib.employeeDetail sd = empView.SelectedItem as EmployeeLib.employeeDetail;
            if (empView.SelectedItem == null)
                ShowMessage(".                                                            الرجاء اختيار موظف للتعديل");
            else
            {
                nama = sd.N;
                Frame.Navigate(typeof(Employee.EditEmployee));
            }
        }
        private void s_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            empView.ItemsSource = EmployeeLib.searchemp(s.Text);
        }

   

    }
    }

