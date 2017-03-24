using SampleTodoXForms.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SampleTodoXForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            // Current.MainPage = new NavigationPage(new MainPage());
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new MainPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                }
            };
        }
    }
}
