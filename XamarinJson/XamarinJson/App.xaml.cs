using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinJson.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinJson
{
    public partial class App : Application
    {
        public App()
        {

#if DEBUG
            LiveReload.Init();
#endif
            InitializeComponent();

            MainPage = new MainView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
