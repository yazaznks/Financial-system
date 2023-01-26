using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Accounting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCredit : Page
    {
 
        public EditCredit()
        {
            this.InitializeComponent();
            List<AccountingLib.CreditDetail> hi = AccountingLib.GetcreditperID(CreditInfo.CID);
            foreach (var type in hi)
            {
                CreditInfo.CID = type.ID;
                cred.SelectedItem = type.CName;
                amount.Text = type.amount.ToString();
                DOB.Date = new DateTimeOffset(new DateTime(int.Parse(type.date.Substring(6, 4)), int.Parse(type.date.Substring(3, 2)), int.Parse(type.date.Substring(0, 2))));
                recipient.Text = type.recip;
                notes.Text = type.notes;
                if (type.petty == true)
                    check.IsChecked = true;
                else
                    check.IsChecked = false;
                
                
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
                ShowMessage(".                                                                  يجب اختيار مصروف");
            }
            else
            {
                if (amount.Text.Equals(""))
                    Frame.Navigate(typeof(HomePage));
                else
                {
                    int am = int.Parse(amount.Text);
                    ShowMessage(".                                   تم اضافة " + am + " ريال إلى مصاريف " + (string)cred.SelectedItem);
                    AccountingLib.UpdateCredit(CreditInfo.CID, (string)cred.SelectedItem, am, notes.Text, recipient.Text, check.IsChecked.Value, DOB.Date.ToString().Substring(0, 10));
                    Frame.Navigate(typeof(CreditInfo));
                }
            }
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            if (cred.SelectedIndex == -1)
            {
                ShowMessage(".                                                                  يجب اختيار مصروف");
            }
            else
            {
                if (amount.Text.Equals(""))
                    Frame.Navigate(typeof(HomePage));
                else
                {
                    int am = int.Parse(amount.Text);
                    ShowMessage(".                                                             تم تعديل المصروف ");
                    AccountingLib.UpdateCredit(CreditInfo.CID, (string)cred.SelectedItem, am, notes.Text, recipient.Text, check.IsChecked.Value, DOB.Date.ToString().Substring(0, 10));
                    Frame.Navigate(typeof(CreditPrint));
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            AccountingLib.DeleteCredit(CreditInfo.CID);
            Frame.Navigate(typeof(CreditInfo));
        }
    }
}
