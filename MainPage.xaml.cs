using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace nedaasqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public void showPane()
        {
            navView.IsPaneVisible = true;
        }
        public void hidePane()
        {
            navView.IsPaneVisible = false;
        }
        public MainPage()
        {
            this.InitializeComponent();
            navView.IsPaneVisible = false;
            Current = this;
        }

        private void navView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Login));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {

            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "Home":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "studentInfo":
                        ContentFrame.Navigate(typeof(studentInfo));
                        break;
                }
            }

        }
        
        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                
                ContentFrame.GoBack();
            }
        }

    }

}
