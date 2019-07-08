using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    using Models;
    using System.Collections.ObjectModel;

    public class LandViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<Currency> currencies;
        #endregion

        #region Propiedades
        public Land Land { get; set; }

        public ObservableCollection<Currency> Currencies
        {
            get
            {
                return this.currencies;
            }
            set
            {
                this.SetValue(ref this.currencies, value);
            }
        }

        #endregion

        #region Constructor
        public LandViewModel(Land land)
        {
            this.Land = land;
            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);
        }
        #endregion
    }
}
