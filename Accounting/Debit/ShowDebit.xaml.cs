using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using nedaasqlite.Accounting.Debit;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class ShowDebit : Page
    {
        public static int DID;
 
        public ShowDebit()
        {
            this.InitializeComponent();
            month.SelectedItem = DateTime.UtcNow.ToString().Substring(3, 2);
            userView.ItemsSource = AccountingLib.GetdebitperM(month.SelectedItem.ToString());
            totalp(); 
        }

        public void totalp()
        {
            int total = 0;
            foreach (var d in AccountingLib.GetdebitperM(month.SelectedItem.ToString()))
            {
                total += d.paid;
            }
            tots.Text = total.ToString();
        }
        private void userView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "SName")
            {
                e.Column.Header = "الإسم";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "registration")
            {
                e.Column.Header = "رسوم التسجيل";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "assess")
            {
                e.Column.Header = "رسوم التقييم";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "academic")
            {
                e.Column.Header = "الرسوم الاكاديمية";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "bus")
            {
                e.Column.Header = "رسوم الباص";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "books")
            {
                e.Column.Header = "رسوم الكتب";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "date")
            {
                e.Column.Header = "تاريخ الدفع";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "total")
            {
                e.Column.Header = "المجموع";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "notes")
            {
                e.Column.Header = "الملاحظات";
        

            }
            if (e.Column.Header.ToString() == "remaining")
            {
                e.Column.Header = "الباقي";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "paid")
            {
                e.Column.Header = "المدفوع";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
        }

        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userView.ItemsSource = AccountingLib.searchTuition(s.Text);
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userView.ItemsSource = AccountingLib.GetdebitperM(month.SelectedItem.ToString());
            totalp();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            AccountingLib.Tuition sd = userView.SelectedItem as AccountingLib.Tuition;
            if (userView.SelectedItem == null)
                ShowMessage(".                                                            الرجاء اختيار الرسوم للتعديل");
            else
            {
                DID = sd.ID;
                Frame.Navigate(typeof(EditDebit));

            }
        }
    }
}
