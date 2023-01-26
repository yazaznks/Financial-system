using nedaasqlite.Accounting;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
   
    public sealed partial class Finance : Page
    {
        public Finance()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            revenue.Text = AccountingLib.GetRevenue().ToString();
            month.SelectedItem = DateTime.UtcNow.ToString().Substring(3, 2);
        }


        private void monthch()
        {
            
            int totalD = 0;
            int totalC = 0;
            List<Data> debit = new List<Data>();
            List<Data> credit = new List<Data>();
            foreach (var d in AccountingLib.GetdebitperM(month.SelectedItem.ToString()))
            {
                
                    debit.Add(new Data() { date = int.Parse(d.date.Substring(0, 2)), Value = totalD + d.paid });
                    totalD += d.paid;
                
            }

            
            foreach (var d in AccountingLib.GetCreditAmount())
            {
                if (d.date.Substring(3, 2) == month.SelectedItem.ToString())
                {
                    totalC += d.amount;
                    credit.Add(new Data() { date = int.Parse(d.date.Substring(0, 2)), Value = totalC  });
                    

                }
            }
            (LineChart.Series[0] as LineSeries).ItemsSource = debit;
            (LineChart.Series[1] as LineSeries).ItemsSource = credit;
            revenueM.Text = (totalD - totalC).ToString();
        }


        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(addCredit));
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreditInfo));
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShowDebit));
        }

        public class Data
        {
            public int date { get; set; }

            public double Value { get; set; }
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            monthch();
        }

        private void gg_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(YearlyReport));
        }

        private void yearT_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(students.YearlyTuition));
        }
    }
}
