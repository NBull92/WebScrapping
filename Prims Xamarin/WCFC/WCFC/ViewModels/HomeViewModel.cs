//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WCFC.ViewModels
{
    using Plugin.Connectivity;
    using Prism.Mvvm;

    public class HomeViewModel : BindableBase
	{
        public HomeViewModel()
        {
            CheckConnectivity();
        }

        private void CheckConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                //Application.Current.MainPage.DisplayAlert("Alert", "No internet connection can be found at this time.", "OK");
            }
        }
    }
}
// WCFC.ViewModels namespace 