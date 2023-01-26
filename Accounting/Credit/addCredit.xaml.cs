using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using nedaasqlite.Accounting;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class addCredit : Page
    {
        public addCredit()
        {
            this.InitializeComponent();
        }

        private void Names_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }



        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            add();
            
        }

        private void amount_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            add();
            if (cred.SelectedIndex != -1 && !amount.Text.Equals(""))
                Frame.Navigate(typeof(CreditPrint));
        }

        private void add()
        {
            if (cred.SelectedIndex == -1)
            {
                ShowMessage(".                                                                  يجب اختيار مصروف");
            }
            else
            {
                if (amount.Text.Equals(""))
                    ShowMessage(".                                                              يجب اختيار مبلغ");
                else
                {
                    int am = int.Parse(amount.Text);
                    ShowMessage(".                                   تم اضافة " + am + " ريال إلى مصاريف "+ (string)cred.SelectedItem );
                    AccountingLib.addCredit((string)cred.SelectedItem, am, notes.Text, recipient.Text, bus.IsChecked.Value);
                    CreditInfo.CID = AccountingLib.GetCreditp(DateTime.UtcNow.ToString().Substring(3, 2)).Last().ID;
                    
                }
            }
        }
    }
}
