using Microsoft.Toolkit.Uwp.UI.Controls;
using nedaasqlite.Accounting;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreditInfo : Page
    {
        public static int CID;
        public static string Dater;
        public CreditInfo()

        {
            this.InitializeComponent();
            month.SelectedItem = DateTime.UtcNow.ToString().Substring(3, 2);
            CreditView.ItemsSource = AccountingLib.GetCreditp(month.SelectedItem.ToString());
            totalp();

        }
        private void btn1_click(object sender, RoutedEventArgs e)
        {
            CreditView.ItemsSource = AccountingLib.GetCreditperM(month.SelectedItem.ToString());
        }

        private void btn2_click(object sender, RoutedEventArgs e)
        {

            CreditView.ItemsSource = AccountingLib.GetCreditp(month.SelectedItem.ToString());
        }

        public void totalp()
        {
            int total = 0;
            foreach (var d in AccountingLib.GetCreditperM(month.SelectedItem.ToString()))
            {
                total += d.amount;
            }
            tots.Text = total.ToString();
        }
        private void CreditView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "CName")
            {
                e.Column.Header = "إسم المصروف";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "amount")
            {
                e.Column.Header = "المبلغ";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "petty")
            {
                e.Column.Header = "نثري";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
         
            }
            if (e.Column.Header.ToString() == "notes")
            {
                e.Column.Header = "الملاحظات";
                
            }
            if (e.Column.Header.ToString() == "date")
            {
                e.Column.Header = "تاريخ الصرف";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "recip")
            {
                e.Column.Header = "اسم المستلم";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
        }
        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            AccountingLib.CreditDetail sd = CreditView.SelectedItem as AccountingLib.CreditDetail;
            if (CreditView.SelectedItem == null)
                ShowMessage(".                                                            الرجاء اختيار مصروف للتعديل");
            else if (sd.ID == 0)
                ShowMessage(".                                           الرجاء اختيار تفاصيل المصاريف ثم تحديد مصروف");
            else
            {
                CID = sd.ID;
                Frame.Navigate(typeof(EditCredit));
                
            }
        }

        private void month_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            CreditView.ItemsSource = AccountingLib.GetCreditp(month.SelectedItem.ToString());
            Dater = month.SelectedItem.ToString();
            totalp();
        }

        private void btnp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pettyCashPrint));
        }
    }
}
