namespace GameLibrary.Import.Txt
{
    public class TextFileGameImporter
    {
        private readonly string _filename;

        public TextFileGameImporter(string filename)
        {
            _filename = filename;
        }

        public Game[] GetGames() =>
            File.ReadAllLines(_filename)
            .Skip(1)
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(BuildGameFromLine)
            .ToArray();

        private static Game BuildGameFromLine(string line)
        {
            string[] parts = line.Split('|', StringSplitOptions.TrimEntries);
            string[] tags = parts[2].Split(",", StringSplitOptions.TrimEntries);
            return new Game(parts[0], parts[1], tags);
        }
    }
}
