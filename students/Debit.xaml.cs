using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Data.SqlClient;
using nedaasqlite.students;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{

    public sealed partial class Debit : Page
    {

        public static bool RegOnly;
        public static bool BusOnly;

        public Debit()
        {

            this.InitializeComponent();
            string sName;
            string connectionString = @"Data Source = NEDA\SQLEXPRESS;Initial Catalog=Neda;User ID=Nedaa;Password=nedaa";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectCMD = "SELECT name FROM student ORDER BY name";
                SqlCommand cmd_getRec = new SqlCommand(selectCMD, con);


                SqlDataReader reader = cmd_getRec.ExecuteReader();
                while (reader.Read())
                {
                    sName = reader.GetString(0);
                    cred.Items.Add(sName);
                }
                con.Close();
            } 
    }
        

        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            RegOnly = regOnly.IsChecked.Value;
            BusOnly = busOnly.IsChecked.Value;
            if (cred.SelectedIndex == -1)
            {
                ShowMessage(".                                                                  يجب اختيار طالب");
            }
            else
            {

                if (assess.Text.Equals(""))
                    assess.Text = "0";
                if (bo.Text.Equals(""))
                    bo.Text = "0";
                int aca = int.Parse(ac.Text);
                int asses = int.Parse(assess.Text);
                int buss = int.Parse(bb.Text);
                int book = int.Parse(bo.Text);
                int remain = int.Parse(tots.Text) - int.Parse(paid.Text);
                int pa = int.Parse(paid.Text);
                if (pa > int.Parse(tots.Text))
                    remain = 0;
                AccountingLib.addTuition((string)cred.SelectedItem, asses, aca, buss, book, notes.Text, remain, pa);
                ShowDebit.DID = AccountingLib.GetdebitperM(DateTime.UtcNow.ToString().Substring(3, 2)).Last().ID;

                if (RegOnly || BusOnly)
                {
                    Frame.Navigate(typeof(Reciept2));
                }
                else
                {
                    Frame.Navigate(typeof(Reciept));
                }
            }
        }


        private void ac_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        CreditInfo CreditInfo { get; set; }


        private void cred_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ac.Text = StudentLib.Getfees(cred.SelectedItem.ToString()).ToString();
            bb.Text = StudentLib.GetBfees(cred.SelectedItem.ToString()).ToString();
            tots.Text = (int.Parse(assess.Text)+ int.Parse(ac.Text)+ int.Parse(bb.Text) + int.Parse(bo.Text)).ToString();
            paid.Text = tots.Text;

        }

        private void BtnS_Click_1(object sender, RoutedEventArgs e)
        {
            ShowMessage(AccountingLib.GetTuitionperName((string)cred.SelectedItem).ToString());
        }

       

        private void ac_TextChanged(object sender, TextChangedEventArgs e)
        {
            tots.Text = (int.Parse(assess.Text) + int.Parse(ac.Text) + int.Parse(bb.Text) + int.Parse(bo.Text)).ToString();
            paid.Text = tots.Text;
        }
    }
}

