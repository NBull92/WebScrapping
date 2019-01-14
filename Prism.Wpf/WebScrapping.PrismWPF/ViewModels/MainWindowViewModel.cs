//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrapping.PrismWPF.ViewModels
{
    using Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Worcester City F.C News";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
// WebScrapping.PrismWPF.ViewModels namespace 