using CommunityToolkit.Mvvm.Input;
using KMA.ProgrammingInCSharp.Practice2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingInCSharp.Practice2.ViewModel
{
    class PersonViewModel : INotifyPropertyChanged

    {
        private Person? _person;

        private RelayCommand? _proceedCommand;

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;
        private bool _taskIsRunning;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FirstName
        {
            get{ return _person.FirstName;}
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public string LastName
        {
            get => _person.LastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public DateTime BirthDate
        {
            get => _person.Birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(BirthDate));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public string Email
        {
            get => _person.Email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public string IsAdultDisplay => _person.IsAdult == true ? "Yes" : "No";
        public string IsBirthdayDisplay => _person.IsBirthday == true ? "Yes" : "No";
        public string WesternSign => _person.SunSign;
        public string ChineseSign => _person.ChineseSign;
        public bool IsProceedEnabled => CanProceedTask();
        public RelayCommand ProceedCommand => _proceedCommand ??= new RelayCommand(async () => await ProceedTask(), () => CanProceedTask());

        private async Task ProceedTask()
        {
            _taskIsRunning = true;
            try
            {
                _person = await Task.Run(() => new Person(_firstName, _lastName, _email, _birthday));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exeption!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally 
            {
                DateTime today = DateTime.Now;
                int age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }

                if (age > 135 || BirthDate > today)
                {
                    _person = null;
                    MessageBox.Show("You entered invalid Date!", "Exeption!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (_person.IsBirthday)
                    {
                        MessageBox.Show("Congrats with ur birthdayyy! It seems like we have a party today!", "Woow, it's a ur day!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    OnPropertyChanged(nameof(FirstName));
                    OnPropertyChanged(nameof(LastName));
                    OnPropertyChanged(nameof(BirthDate));
                    OnPropertyChanged(nameof(Email));
                    OnPropertyChanged(nameof(IsAdultDisplay));
                    OnPropertyChanged(nameof(IsBirthdayDisplay));
                    OnPropertyChanged(nameof(WesternSign));
                    OnPropertyChanged(nameof(ChineseSign));

                }
                _taskIsRunning = false;
            }


        }

        private bool CanProceedTask() 
        {
            return !_taskIsRunning && !string.IsNullOrEmpty(_firstName) && !string.IsNullOrEmpty(_lastName) && !string.IsNullOrEmpty(_email) && _birthday != default;
        }


    }
     
    }

