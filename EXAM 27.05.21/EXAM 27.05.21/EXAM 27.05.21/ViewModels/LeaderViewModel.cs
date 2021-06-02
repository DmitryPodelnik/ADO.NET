using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EXAM_27._05._21.Models;

namespace EXAM_27._05._21.ViewModels
{
    class LeaderViewModel : INotifyPropertyChanged
    {
        private Leader _selectedLeader;
        public ObservableCollection<Leader> Leaders { get; set; }
        public Leader SelectedGroup
        {
            get { return _selectedLeader; }
            set
            {
                _selectedLeader = value;
                OnPropertyChanged("SelectedLeader");
            }
        }

        public LeaderViewModel()
        {
            Leaders = new();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
