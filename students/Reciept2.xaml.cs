using Microsoft.Toolkit.Uwp.Helpers;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using nedaasqlite.students;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.students
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Reciept2 : Page
    {
        public Reciept2()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bool regO = Debit.RegOnly;
            bool busO = Debit.BusOnly;
            int assess;
            int buss;
            List<AccountingLib.Tuition> hi = AccountingLib.GetDebitperID(ShowDebit.DID);
            foreach (var type in hi)
            {
                assess = type.assess;
                buss = type.bus;
                student.Text = type.SName;
                reg.Text = assess.ToString();
                date.Text = type.date;
                bus.Text = buss.ToString();
                notesg.Text = type.notes;
                bill.Text = type.ID.ToString();
                total.Text = type.total.ToString();
                if (regO && !busO)
                {
                    busbb.Visibility = Visibility.Collapsed;
                    busb.Visibility = Visibility.Collapsed;
                    bus.Visibility = Visibility.Collapsed;
                    bus2.Visibility = Visibility.Collapsed;


                }
             


            }
            var printHelper = new PrintHelper(print);
            printHelper.ShowPrintUIAsync("Nedaa app", true);

        }
    }
}