namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributos
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filtro;
        private List<Land> LandsList;
        #endregion

        #region Propiedades
        public ObservableCollection<LandItemViewModel> Lands
        {
            get
            {
                return this.lands;
            }
            set { SetValue(ref this.lands, value); }
        }
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filtro
        {
            get
            {
                return this.filtro;
            }
            set
            {
                SetValue(ref this.filtro, value);
                this.Buscar();
            }
        }
        #endregion

        #region Constructores
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Metodos
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar"
                    );
                await Application.Current.MainPage.Navigation.PopAsync();
                this.IsRefreshing = false;
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu"
                , "/rest"
                , "/v2/all");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                this.IsRefreshing = false;
                return;
            }

            this.LandsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<LandItemViewModel>(
                this.ToLandItemViewModel());

            this.IsRefreshing = false;
        }

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return this.LandsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations
            });
        }

        private void Buscar()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filtro.ToLower())
                        || l.Capital.ToLower().Contains(this.Filtro.ToLower())));
            }
        }
        #endregion

        #region Comandos
        public ICommand RefrescarCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }
        public ICommand BuscarCommand
        {
            get
            {
                return new RelayCommand(Buscar);
            }
        }
        #endregion

    }
}
