using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Accounting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pettyCashPrint : Page
    {
        string datee = CreditInfo.Dater;
        int totals = 0;
        public pettyCashPrint()
        {
            this.InitializeComponent();
            CreditView.ItemsSource = AccountingLib.Getpetty(datee);
            date.Text = datee;
            totalp();
            var printHelper = new PrintHelper(print);
            printHelper.ShowPrintUIAsync("Nedaa app", true);

        }

        public void totalp()
        {
            foreach (var d in AccountingLib.Getpetty(datee))
            {
                totals += d.amount;
            }
            total.Text = totals.ToString();
        }

        private void CreditView_AutoGeneratingColumn(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "CName")
            {
                e.Column.Header = "إسم المصروف";
              
            }
            if (e.Column.Header.ToString() == "amount")
            {
                e.Column.Header = "المجموع";
               
            }
            if (e.Column.Header.ToString() == "petty")
            {
                e.Cancel = true;

            }
            if (e.Column.Header.ToString() == "notes")
            {
                e.Cancel = true;

            }
            if (e.Column.Header.ToString() == "date")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "recip")
            {
                e.Cancel = true;
            }
        }
    }
}
