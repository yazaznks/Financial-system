
using System;
using System.Data.SqlClient;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Employee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class salaryC : Page
    {
        double WD;
        double AH;
        double AD;
        double final;
        public static string connectionString = @"Data Source = NEDA\SQLEXPRESS;Initial Catalog=Neda;User ID=Nedaa;Password=nedaa";
        public salaryC()
        {
            this.InitializeComponent();

            string sName;
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT name FROM Employee";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    sName = reader.GetString(0);
                    cred.Items.Add(sName);
                }
                con.Close();
            }
            for (int i = 1; i < 13; i++)
            {
                dat.Items.Add(i);
            }

        }



        private void cred_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            total.Text = calculate();

        }
        private void ac_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private string calculate()
        {
            ac.Text = EmployeeLib.GetSalary(cred.SelectedItem.ToString()).ToString();
            int salary = int.Parse(ac.Text);
            if (days.Text == "")
                return "اختر ايام العمل";
            if (absent.Text == "")
                return "اختر ايام الغياب";
            if (hours.Text == "")
                return "اختر ساعات التأخير";

            WD = Convert.ToDouble(days.Text);
            AH = Convert.ToDouble(hours.Text) / 10.0;
            AD = double.Parse(absent.Text) + AH;
            final = salary / WD * (WD - AD);
            final = Math.Round(final, 0);
            return final.ToString();
        }

        private void days_TextChanged(object sender, TextChangedEventArgs e)
        {
            total.Text = calculate();
        }
        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            if (dat.SelectedIndex == -1)
                ShowMessage(".                                                       تم اضافة مرتب " + "2" + " بنجاح");
            else
            {
                string name = cred.SelectedItem.ToString();
                string datee = dat.SelectedItem.ToString();
                EmployeeLib.addSalary(name, final, datee, AD, notes.Text);
                ShowMessage(" تم اضافة مرتب" + name +" بنجاح                    .");
            }
        }
        private void print_Click(object sender, RoutedEventArgs e)
        {
            if (dat.SelectedIndex == -1)
                ShowMessage(".                                                            الرجاء اختيار شهر");
            else
            {
                string name = cred.SelectedItem.ToString();
                string datee = dat.SelectedItem.ToString();
                EmployeeLib.addSalary(name, final, datee, AD, notes.Text);
                Frame.Navigate(typeof(SalaryPrint));
            }
        }
    }
}
