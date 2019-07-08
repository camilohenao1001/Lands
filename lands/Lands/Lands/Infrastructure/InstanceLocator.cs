using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.Infrastructure
{
    using ViewModels;
    public class InstanceLocator
    {
        #region Propiedades
        public MainViewModel Main { get; set; }
        #endregion

        #region Constructores
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
