namespace App.ViewModels
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class PaisesViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;
        #endregion


        #region Attributes

        private ObservableCollection<Paises> pais;
        private bool isRefreshing;
        #endregion


        #region Properties
        public ObservableCollection<Paises> Lands
        {
            get { return this.pais; }
            set { SetValue(ref this.pais, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion



        #region Constructors

        public PaisesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }

        #endregion


        #region Methods
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                   "Error", connection.Message,
                   "Aceptar");
                return;
                await Application.Current.MainPage.Navigation.PopAsync();
            }

            var response = await this.apiService.GetList<Paises>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error", response.Message,
                    "Aceptar");
                return;
            }
            var list = (List<Paises>)response.Result;
            this.Lands = new ObservableCollection<Paises>(list);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);

            }
        }
        #endregion
    }
}
