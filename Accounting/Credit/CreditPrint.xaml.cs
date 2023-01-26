using Microsoft.Toolkit.Uwp.Helpers;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreditPrint : Page
    {
        public CreditPrint()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            List<AccountingLib.CreditDetail> hi = AccountingLib.GetcreditperID(CreditInfo.CID);
            foreach (var type in hi)
            {

                credit.Text = type.CName;
                amount.Text = type.amount.ToString();
                date.Text = type.date;
                reciepient.Text = type.recip;
                notes.Text = type.notes;
                bill.Text = type.ID.ToString();
            }
            var printHelper = new PrintHelper(print);
            printHelper.ShowPrintUIAsync("Nedaa app", true);
        }
    }
}
