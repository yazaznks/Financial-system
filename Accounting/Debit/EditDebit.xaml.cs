using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Accounting.Debit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditDebit
    {
        public EditDebit()
        {
            this.InitializeComponent();
            foreach (var tuit in AccountingLib.GetDebitperID(ShowDebit.DID)) {
                cred.Items.Add(tuit.SName);
                DOB.Date = new DateTimeOffset(new DateTime(int.Parse(tuit.date.Substring(6, 4)), int.Parse(tuit.date.Substring(3, 2)), int.Parse(tuit.date.Substring(0, 2))));
                cred.SelectedItem = tuit.SName;
                ac.Text = tuit.academic.ToString();
                assess.Text = tuit.assess.ToString();
                bb.Text = tuit.bus.ToString();
                bo.Text = tuit.books.ToString();
                pai.Text = tuit.paid.ToString();
                tots.Text = (tuit.academic + tuit.assess + tuit.books + tuit.bus).ToString();
                rema.Text = tuit.remaining.ToString();
                notes.Text = tuit.notes.ToString();
                paid.Text = tuit.paid.ToString();
                
            }
        }
        private void ac_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }    

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            
            AccountingLib.UpdateDebit(ShowDebit.DID, cred.SelectedItem.ToString(), int.Parse(assess.Text), int.Parse(ac.Text), int.Parse(bb.Text), int.Parse(bo.Text), DOB.Date.ToString().Substring(0, 10),int.Parse(paid.Text), int.Parse(rema.Text), notes.Text );
            Frame.Navigate(typeof(Reciept));
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            AccountingLib.DeleteDebit(ShowDebit.DID);
            Frame.Navigate(typeof(ShowDebit));
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
           
            AccountingLib.UpdateDebit(ShowDebit.DID, cred.SelectedItem.ToString(), int.Parse(assess.Text), int.Parse(ac.Text), int.Parse(bb.Text), int.Parse(bo.Text), DOB.Date.ToString().Substring(0, 10), int.Parse(paid.Text), int.Parse(rema.Text), notes.Text);
            Frame.Navigate(typeof(ShowDebit));
        }
    }
}
