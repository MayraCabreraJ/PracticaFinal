using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App
    

{
    using ViewModels;
    using View;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Lands = new PaisesViewModel();

            this.MainPage = new NavigationPage(new PaisesPage());
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
