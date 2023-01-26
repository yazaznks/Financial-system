using System;
using System.Data.SqlClient;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddStudent : Page
    {
        public AddStudent()
        {
            this.InitializeComponent();
            DOB.Date = new DateTimeOffset(new DateTime(2050, 01, 01));
        }

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            if (busF.Text.Equals(""))
                busF.Text = "0";
            int b = int.Parse(busF.Text);
            if (tuition.Text.Equals(""))
                tuition.Text = "0";
            int t = int.Parse(tuition.Text);
            StudentLib.addStudent(Names.Text, DOB.Date.ToString().Substring(0,10), ID.Text, Mom.Text, Dad.Text, disability.Text,
                nationality.Text, t, b, notes.Text);
            Frame.Navigate(typeof(HomePage));
        }
    }
}
