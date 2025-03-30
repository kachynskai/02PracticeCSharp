using CommunityToolkit.Mvvm.Input;
using KMA.ProgrammingInCSharp.Practice2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Practice2.ViewModel
{
    class PersonViewModel : INotifyPropertyChanged

    {
        private Person? _person;

        private RelayCommand? _proceedCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
     
    }

