using CommunityToolkit.Mvvm.Input;
using KMA.ProgrammingInCSharp.Practice2.Exceptions;
using KMA.ProgrammingInCSharp.Practice2.Model;
using System;
using System.ComponentModel;
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
        private DateTime? _birthday;
        private bool _taskIsRunning;
      

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _proceedCommand?.NotifyCanExecuteChanged();
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public DateTime? BirthDate
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(BirthDate));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

  
        public string DisplayFirstName => _person?.FirstName ?? string.Empty;
        public string DisplayLastName => _person?.LastName ?? string.Empty;
        public string DisplayEmail => _person?.Email ?? string.Empty;
        public string DisplayBirthDate => _person?.Birthday.ToString("d") ?? string.Empty;
        public string IsAdultDisplay => _person != null ? (_person.IsAdult ? "Yes" : "No") : string.Empty;
        public string IsBirthdayDisplay => _person != null ? (_person.IsBirthday ? "Yes" : "No") : string.Empty;
        public string WesternSign => _person?.SunSign ?? string.Empty;
        public string ChineseSign => _person?.ChineseSign ??  string.Empty;

        public bool IsProceedEnabled => CanProceedTask();

        public RelayCommand ProceedCommand => _proceedCommand ??= new RelayCommand(async () => await ProceedTask(), () => CanProceedTask());

        private async Task ProceedTask()
        {
            _taskIsRunning = true;
            OnPropertyChanged(nameof(IsProceedEnabled));

            try
            {
                
                _person = await Task.Run(() => new Person(_firstName, _lastName, _email, _birthday.Value));

                if (_person.IsBirthday)
                {
                    MessageBox.Show("Congrats with ur birthdayyy! It seems like we have a party today!", "Woow, it's a ur day!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                OnPropertyChanged(nameof(DisplayFirstName));
                OnPropertyChanged(nameof(DisplayLastName));
                OnPropertyChanged(nameof(DisplayEmail));
                OnPropertyChanged(nameof(DisplayBirthDate));
                OnPropertyChanged(nameof(IsAdultDisplay));
                OnPropertyChanged(nameof(IsBirthdayDisplay));
                OnPropertyChanged(nameof(WesternSign));
                OnPropertyChanged(nameof(ChineseSign));
            }
            catch (UnbornPersonException ex)
            {
                _birthday = null;
                OnPropertyChanged(nameof(BirthDate));
                MessageBox.Show(ex.Message, "Exeption!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TooOldPersonException ex)
            {
                _birthday = null;
                OnPropertyChanged(nameof(BirthDate));
                MessageBox.Show(ex.Message, "Exeption!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidEmailException ex)
            {
                _email = string.Empty;
                OnPropertyChanged(nameof(Email));
                MessageBox.Show(ex.Message, "Exeption!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _taskIsRunning = false;
                OnPropertyChanged(nameof(IsProceedEnabled));
            }
        }

        private bool CanProceedTask()
        {
            return !_taskIsRunning &&
                   !string.IsNullOrWhiteSpace(_firstName) &&
                   !string.IsNullOrWhiteSpace(_lastName) &&
                   !string.IsNullOrWhiteSpace(_email) &&
                   _birthday.HasValue;
        }
    }
}
