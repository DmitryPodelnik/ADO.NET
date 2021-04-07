using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Connected_Layer_04._04._21
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private ModelCustom _selectedCustom;
        private ModelSale _selectedSale;
        private ModelSalesperson _selectedSalesperson;

        public ObservableCollection<ModelCustom> ModelCustoms { get; set; }
        public ObservableCollection<ModelSale> ModelSales { get; set; }
        public ObservableCollection<ModelSalesperson> ModelSalespeople { get; set; }

        public ModelCustom SelectedCustom
        {
            get => _selectedCustom;
            set
            {
                _selectedCustom = value;
                OnPropertyChanged("SelectedCustom");
            }
        }
        public ModelSale SelectedSale
        {
            get => _selectedSale;
            set
            {
                _selectedSale = value;
                OnPropertyChanged("SelectedSale");
            }
        }
        public ModelSalesperson SelectedSalesperson
        {
            get => _selectedSalesperson;
            set
            {
                _selectedSalesperson = value;
                OnPropertyChanged("SelectedSalesperson");
            }
        }

        public ApplicationViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
