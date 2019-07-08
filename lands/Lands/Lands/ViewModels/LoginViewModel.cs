namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;

    public class LoginViewModel : BaseViewModel
    {
        
        #region Atibutos
        private string usuario;
        private string clave;
        private bool isRunning;
        private bool isRemembered;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public string Usuario
        {
            get{return this.usuario;}
            set{SetValue(ref this.usuario, value);}
        }
        public string Clave
        {
            get { return this.clave; }
            set { SetValue(ref this.clave, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemembered
        {
            get { return this.isRemembered; }
            set { SetValue(ref this.isRemembered, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructores
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
            this.usuario = "asistente.sion";
            this.clave = "soporte";
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "¡Incompleto!", "Por favor ingrese el usuario", "Aceptar");
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(this.Clave))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "¡Incompleto!", "Por favor ingrese la clave", "Aceptar");
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            if (this.Usuario != "asistente.sion" || this.Clave != "soporte")
            {
                await Application.Current.MainPage.DisplayAlert(
                   "¡Incorrecto!", "Usuario o clave incorrectos", "Aceptar");
                this.Clave = string.Empty;
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Usuario = string.Empty;
            this.Clave = string.Empty;

            MainViewModel.GetInstance().Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }
        #endregion

    }
}
