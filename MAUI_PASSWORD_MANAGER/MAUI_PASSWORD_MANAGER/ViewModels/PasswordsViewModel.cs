using CommunityToolkit.Mvvm.Input;
using MAUI_PASSWORD_MANAGER.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MAUI_PASSWORD_MANAGER.ViewModels;

internal class PasswordsViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.PasswordViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public PasswordsViewModel()
    {
        AllNotes = new ObservableCollection<ViewModels.PasswordViewModel>(Models.Password.LoadAll().Select(n => new PasswordViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<ViewModels.PasswordViewModel>(SelectNoteAsync);
    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.PasswordPage));
    }

    private async Task SelectNoteAsync(ViewModels.PasswordViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.PasswordPage)}?load={note.Identifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            PasswordViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            PasswordViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new PasswordViewModel(Models.Password.Load(noteId)));
        }
    }
}
