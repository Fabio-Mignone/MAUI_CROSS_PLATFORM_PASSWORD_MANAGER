using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace MAUI_PASSWORD_MANAGER.ViewModels
{
    internal class PasswordGeneratorViewModel : INotifyPropertyChanged
    {
        private int passwordLength = 8;
        private bool includeUppercase;
        private bool includeLowercase;
        private bool includeNumbers;
        private bool includeSpecialChars;
        private string generatedPassword;
        public int PasswordLength
        {
            get { return passwordLength; }
            set
            {
                // Imposta il valore minimo a 8
                if (value < 8)
                    passwordLength = 8;
                else
                    passwordLength = value;
                OnPropertyChanged();
            }
        }
        public ICommand CopyToClipboardCommand { get; private set; }
        private void CopyToClipboard()
        {
            if (!string.IsNullOrWhiteSpace(GeneratedPassword))
            {
                Clipboard.SetTextAsync(GeneratedPassword);
                // Puoi aggiungere qui la gestione degli avvisi o delle notifiche se necessario
            }
        }

        public bool IncludeUppercase
        {
            get { return includeUppercase; }
            set { SetProperty(ref includeUppercase, value); }
        }

        public bool IncludeLowercase
        {
            get { return includeLowercase; }
            set { SetProperty(ref includeLowercase, value); }
        }

        public bool IncludeNumbers
        {
            get { return includeNumbers; }
            set { SetProperty(ref includeNumbers, value); }
        }

        public bool IncludeSpecialChars
        {
            get { return includeSpecialChars; }
            set { SetProperty(ref includeSpecialChars, value); }
        }

        public string GeneratedPassword
        {
            get { return generatedPassword; }
            set { SetProperty(ref generatedPassword, value); }
        }

        public ICommand GeneratePasswordCommand { get; private set; }

        public PasswordGeneratorViewModel()
        {
            GeneratePasswordCommand = new Command(GeneratePassword);
            CopyToClipboardCommand = new Command(CopyToClipboard);
        }
        private void GeneratePassword()
        {
            if (!IncludeUppercase && !IncludeLowercase && !IncludeNumbers && !IncludeSpecialChars)
            {
                // Nessuna opzione selezionata, mostra un messaggio all'utente
                GeneratedPassword = "Seleziona almeno una opzione";
                return;
            }

            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?";

            StringBuilder validChars = new StringBuilder();
            if (IncludeLowercase)
                validChars.Append(lowercaseChars);
            if (IncludeUppercase)
                validChars.Append(uppercaseChars);
            if (IncludeNumbers)
                validChars.Append(numberChars);
            if (IncludeSpecialChars)
                validChars.Append(specialChars);

            char[] password = new char[PasswordLength];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomData = new byte[PasswordLength];
                rng.GetBytes(randomData);
                for (int i = 0; i < PasswordLength; i++)
                {
                    int index = randomData[i] % validChars.Length;
                    password[i] = validChars[index];
                }
            }

            GeneratedPassword = new string(password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
