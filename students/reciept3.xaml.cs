using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class reciept3 : Page
    {
        public reciept3()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            int tuition;
            int assess;
            int buss;
            int books;

            List<AccountingLib.Tuition> hi = AccountingLib.GetDebitperID(ShowDebit.DID);
            foreach (var type in hi)
            {
                List<StudentLib.studentDetail> Id = StudentLib.searchStudent(type.SName);
                foreach (var info in Id)
                {
                    IDD.Text = info.ID;
                }
                assess = type.assess;
                buss = type.bus;
                books = type.books;
                tuition = type.academic;
                student.Text = type.SName;
                note.Text = YearlyTuition.notes2;
                reg.Text = assess.ToString();
                date.Text = type.date;
                bus.Text = buss.ToString();
                book.Text = books.ToString();
                notesg.Text += type.notes;
                if (tuition > 6000)
                    Tuition.Text = tuition.ToString();
                bill.Text = type.ID.ToString();
                disc.Text = YearlyTuition.disco.ToString();
                total.Text = (type.total - int.Parse(disc.Text)).ToString();
                


            }
            var printHelper = new PrintHelper(print);
            printHelper.ShowPrintUIAsync("Nedaa app", true);

        }
    }
}
  
