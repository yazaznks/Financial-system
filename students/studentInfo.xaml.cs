

using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{

    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class studentInfo : Page
    {

        public studentInfo()
        {
            this.InitializeComponent();
            userView.ItemsSource = StudentLib.GetStudent();
            

        }
        private void dataGrid1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "name")
            {
                e.Column.Header = "الإسم";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "dob")
            {
                e.Column.Header = "تاريخ الميلاد";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "ID")
            {
                e.Column.Header = "الرقم الشخصي";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "momNumber")
            {
                e.Column.Header = "رقم الام";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "dadNumber")
            {
                e.Column.Header = "رقم الاب";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "condition")
            {
                e.Column.Header = "الحالة";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "nationality")
            {
                e.Column.Header = "الجنسية";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "fees")
            {
                e.Column.Header = "الرسوم";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "BusF")
            {
                e.Column.Header = "رسوم الباص";
                e.Column.Width = Microsoft.Toolkit.Uwp.UI.Controls.DataGridLength.Auto;
            }
            if (e.Column.Header.ToString() == "notes")
            {
                e.Column.Header = "الملاحظات";
            }
            if (e.Column.Header.ToString() == "N")
            {
                e.Cancel = true;
            }
        }
        public static int nama;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userView.ItemsSource = StudentLib.searchStudent(s.Text);
        }
        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }
        private void btn3_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            StudentLib.studentDetail sd = userView.SelectedItem as StudentLib.studentDetail;
            if (userView.SelectedItem == null)
                ShowMessage(".                                                            الرجاء اختيار طالب للتعديل");
            else
            {
                nama = sd.N;
                Frame.Navigate(typeof(students.editS));
            }
        }

       
}
    }

