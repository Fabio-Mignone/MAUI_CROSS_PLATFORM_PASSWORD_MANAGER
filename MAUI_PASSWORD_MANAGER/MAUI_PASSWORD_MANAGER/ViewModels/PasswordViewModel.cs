using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MAUI_PASSWORD_MANAGER.ViewModels;

internal class PasswordViewModel : ObservableObject, IQueryAttributable
{
    private Models.Password _note;

    public string Text
    {
        get => _note.Text;
        set
        {
            if (_note.Text != value)
            {
                _note.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime Date => _note.Date;

    public string Identifier => _note.Filename;

    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public PasswordViewModel()
    {
        _note = new Models.Password();
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public PasswordViewModel(Models.Password note)
    {
        _note = note;
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        _note.Date = DateTime.Now;
        _note.Save();
        await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
    }

    private async Task Delete()
    {
        _note.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _note = Models.Password.Load(query["load"].ToString());
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = Models.Password.Load(_note.Filename);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
    }
}