using System.Collections.Generic;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.Accounting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YearlyReport : Page
    {
        List<Data> debit = new List<Data>();

        
        public YearlyReport()
        {
            this.InitializeComponent();
            calculate();
            userView.ItemsSource = debit;
       
        }

        private void calculate()
        {
            Data d1 = new Data() {  };
            

            for (int i = 1; i < 13; i++)
            {
                string datee = "";
                if (i < 10)
                    datee = "0" + i.ToString();
                else
                    datee = i.ToString();
                double Debite = AccountingLib.GetTuitionTotalM(datee); ;
                double bus1p = 0;
                double bus2p = 0;
                double bus1f = 0;
                double bus2f = 0;
                double hosp = 0;
                double needs = 0;
                double book = 0;
                double phones = 0;
                double paperw = 0;
                double bank = 0;
                double salary = 0;
                double house = 0;
                double camera = 0;
                double insurance = 0;
                double rent = 0;
                double electricity = 0;
                double internet = 0;
                double ads = 0;
                double Total = 0;
                
                foreach (var d in AccountingLib.GetCreditperM(datee))
                {

                    if (d.CName == "بترول باص مجدي 293995")
                    {
                        bus1p = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "بترول باص ادريس 276148")
                    {
                        bus2p = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "صيانة باص مجدي 293995")
                    {
                        bus1f = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "صيانة باص ادريس 276148")
                    {
                        bus2f = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "مصاريف مكتب وضيافة")
                    {
                        hosp = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "لوازم المركز")
                    {
                        needs = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "قرطاسية")
                    {
                        book = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "اتصالات")
                    {
                        phones = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "تخليص أوراق")
                    {
                        paperw = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "مصاريف بنكية")
                    {
                        bank = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "مرتبات")
                    {
                        salary = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "سكن موظفين")
                    {
                        house = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "كاميرات مراقبة وانتركم")
                    {
                        camera = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "تأمين وعقود صيانة")
                    {
                        insurance = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "ايجار الفيلا")
                    {
                        rent = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "كهرماء")
                    {
                        electricity = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "انترنت")
                    {
                        internet = d.amount;
                        Total += d.amount;
                    }
                    else if (d.CName == "اعلانات ودعاية")
                    {
                        ads = d.amount;
                        Total += d.amount;
                    }
                }
                
                debit.Add(new Data() { Debit=Debite, date = datee, bus1p = bus1p, bus2p = bus2p, bus1f = bus1f, bus2f=bus2f, hosp = hosp, needs = needs, books = book, phones = phones, paperw = paperw, banking = bank, salaries = salary, house = house,
                camera = camera, insurance = insurance, rent = rent, electricity=electricity, internet=internet,ads=ads, Total=Total});
                
            }


        }
        public class Data
        {
            public string date { get; set; }
            public double Debit { get; set; }
            public double bus1p { get; set; }
            public double bus2p { get; set; }
            public double bus1f { get; set; }
            public double bus2f { get; set; }
            public double hosp { get; set; }
            public double needs { get; set; }
            public double books { get; set; }
            public double phones { get; set; }
            public double paperw { get; set; }
            public double banking { get; set; }
            public double salaries { get; set; }
            public double house { get; set; }
            public double camera { get; set; }
            public double insurance { get; set; }
            public double rent { get; set; }
            public double electricity { get; set; }
            public double internet { get; set; }
            public double ads { get; set; }
            public double Total { get; set; }

        }

        private void userView_AutoGeneratingColumn(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            
            if (e.Column.Header.ToString() == "date")
            {
                e.Column.Header = "الشهر";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "Debit")
            {
                e.Column.Header = "مجموع\nالايرادات";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "bus1p")
            {
                e.Column.Header = "بترول باص\n293995";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "bus2p")
            {
                e.Column.Header = "بترول باص\n276148";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "bus1f")
            {
                e.Column.Header = "صيانة باص\n293995";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "bus2f")
            {
                e.Column.Header = "بترول باص\n276148";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "hosp")
            {
                e.Column.Header = "ضيافة";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "needs")
            {
                e.Column.Header = "لوازم";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "books")
            {
                e.Column.Header = "قرطاسية";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "phones")
            {
                e.Column.Header = "اتصالات";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "paperw")
            {
                e.Column.Header = "تخليص\nاوراق";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "banking")
            {
                e.Column.Header = "مصاريف\nبنكية";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "salaries")
            {
                e.Column.Header = "مرتبات";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "house")
            {
                e.Column.Header = "سكن موظفين";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "camera")
            {
                e.Column.Header = "كاميرات\nمراقبة";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "insurance")
            {
                e.Column.Header = "تأمين\nوعقود";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "rent")
            {
                e.Column.Header = "إيجار\nفيلا";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "electricity")
            {
                e.Column.Header = "كهرماء";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "internet")
            {
                e.Column.Header = "انترنت";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "ads")
            {
                e.Column.Header = "اعلان\nودعاية";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "Total")
            {
                e.Column.Header = "مجموع\nالمصروفات";
             
            }
        }

    }
}