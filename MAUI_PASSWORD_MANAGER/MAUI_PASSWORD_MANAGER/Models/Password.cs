namespace MAUI_PASSWORD_MANAGER.Models
{
    internal class Password
    {
        public Password()
        {
            Filename = $"{Path.GetRandomFileName()}.notes.txt";
            Date = DateTime.Now;
            Text = "";
        }
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public static Password Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return
                new()
                {
                    Filename = Path.GetFileName(filename),
                    Text = File.ReadAllText(filename),
                    Date = File.GetLastWriteTime(filename)
                };
        }
        public static IEnumerable<Password> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(appDataPath, "*.notes.txt")

                    // Each file name is used to load a note
                    .Select(filename => Password.Load(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by date
                    .OrderByDescending(note => note.Date);
        }
        public void Save() =>
        File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), Text);
        public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename));
    }
}
    
