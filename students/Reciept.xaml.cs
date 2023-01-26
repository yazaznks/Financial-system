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
    public sealed partial class Reciept : Page
    {
        public Reciept()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
             
            int tuition;
            int assess;
            int buss;
            int books;
            int subt;
            List<AccountingLib.Tuition> hi = AccountingLib.GetDebitperID(ShowDebit.DID);
            foreach (var type in hi)
            {
                assess = type.assess;
                buss = type.bus;
                books = type.books;
                tuition = type.academic;
                student.Text = type.SName;

                reg.Text = assess.ToString();
                date.Text = type.date;
                bus.Text = buss.ToString();
                book.Text = books.ToString();
                notesg.Text = type.notes;
                if (tuition > 6000)
                    Tuition.Text = tuition.ToString();

                bill.Text = type.ID.ToString();
                subt = int.Parse(Tuition.Text) + assess + buss + books;
                sub.Text = subt.ToString();
                discount.Text = (subt - type.total).ToString();
                total.Text = type.total.ToString();
                if (type.paid < type.total)
                {
                    notesg.Text += "المدفوع : " + type.paid + "\n الباقي : "+ (type.total - type.paid);
                    AccountingLib.Updatenote(type.ID, notesg.Text);
                }
                else if (type.paid > type.total)
                {

                    notesg.Text =  " المدفوع : " + type.paid + "\n" + type.notes;
                    AccountingLib.Updatenote(type.ID, notesg.Text);
                }
            

            }
            var printHelper = new PrintHelper(print);
            printHelper.ShowPrintUIAsync("Nedaa app", true);
        
        }
    }
}