using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.students
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YearlyTuition : Page
    {
        public static int disco;
        public static string notes2;
        public YearlyTuition()
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
                int remain = 0;
                disco = int.Parse(disc.Text);
                int pa = aca + asses + buss + book - int.Parse(disc.Text);
                string note = DateF.Date.ToString().Substring(0, 10) + " إلى " + DateT.Date.ToString().Substring(0, 10);
                notes2 = notes.Text;


                AccountingLib.addTuition((string)cred.SelectedItem, asses, aca, buss, book, note, remain, pa);
                ShowDebit.DID = AccountingLib.GetdebitperM(DateTime.UtcNow.ToString().Substring(3, 2)).Last().ID;

               
                Frame.Navigate(typeof(reciept3));
                
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
            tots.Text = (int.Parse(assess.Text) + int.Parse(ac.Text) + int.Parse(bb.Text) + int.Parse(bo.Text)).ToString();
           

        }

        private void BtnS_Click_1(object sender, RoutedEventArgs e)
        {
            ShowMessage(AccountingLib.GetTuitionperName((string)cred.SelectedItem).ToString());
        }



        private void ac_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (assess.Text.Equals(""))
                assess.Text = "0";
            if (bo.Text.Equals(""))
                bo.Text = "0";
            if (bb.Text.Equals(""))
                bo.Text = "0";
            if (disc.Text.Equals(""))
                bo.Text = "0";
            tots.Text = (int.Parse(assess.Text) + int.Parse(ac.Text) + int.Parse(bb.Text) + int.Parse(bo.Text) - int.Parse(disc.Text)).ToString();
            
        }
    }
}
        
    

