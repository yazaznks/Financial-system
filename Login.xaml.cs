using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public static string permission;
        public Login()
        {
            this.InitializeComponent();
            MainPage.Current.hidePane();
            foreach (var n in EmployeeLib.GetUsers())
            {
                user.Items.Add(n.Name);
            }
        }
        private async void ShowMessage(string str)
        {
            var dialog = new MessageDialog(str);
            await dialog.ShowAsync();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (user.SelectedIndex == -1)
            {
                ShowMessage(".                                                                  يجب اختيار مستخدم");
            }
            else
            {
                if (EmployeeLib.validate(user.SelectedItem.ToString(), pass.Password.ToString()) == "0")
                    ShowMessage(".                                                         كلمة المرور خاطئة");
                else if (EmployeeLib.validate(user.SelectedItem.ToString(), pass.Password.ToString()) == "1")
                {
                    permission = "1";
                    MainPage.Current.showPane();
                    Frame.Navigate(typeof(HomePage));

                }
                else if (EmployeeLib.validate(user.SelectedItem.ToString(), pass.Password.ToString()) == "2")
                {
                    permission = "2";
                    MainPage.Current.showPane();
                    Frame.Navigate(typeof(HomePage));
                }
            }
        }
    }
}
