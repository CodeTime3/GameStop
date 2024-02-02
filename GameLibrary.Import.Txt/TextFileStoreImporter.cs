using GameLibrary;

namespace GameLibrary.Import.Txt
{
    public class TextFileStoreImporter
    {
        private readonly string _pathFile;
        //fare altre 2 classi per game e platform
        public TextFileStoreImporter(string pathFile)
        {
            _pathFile = pathFile;
        }

        public Store[] GetStores() //con la lista permette di gestire diversi casi più facilmente
        {
            string[] lines = File.ReadAllLines(_pathFile);
            List<Store> result = new List<Store>();

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];

                if (string.IsNullOrEmpty(line)) //se trova una stringa vuota salta al prossimo elemento 
                {
                    continue;
                }

                string[] parts = line.Split('|', StringSplitOptions.TrimEntries ); //divisione in base al '|'
                Uri? uri = string.IsNullOrEmpty(parts[2]) ? null : new Uri(parts[2]); //trasformazione parte 2 in uri
                var store = new Store(parts[0], parts[1], uri);
                result.Add(store);
            }

            return result.ToArray(); //trasforma result in un array
        }

        //linq con le stringhe
        public Store[] GetStores3() => 
            File
            .ReadAllLines(_pathFile)
            .Skip(1) //quante ne vuoi saltare, salta la prima
            .Where(line => !string.IsNullOrEmpty(line)) //esclude le righe vuote //con il where bisogna ritornare un bool
            .Distinct() 
            .Select(BuildStoreFromLine)//mappare da una sequenza a un'altra, nel nostro caso traforma delle stringhe in un'ogetto store
            .ToArray();

        private static Store BuildStoreFromLine( string line )
        {
            string[] parts = line.Split('|', StringSplitOptions.TrimEntries); //divisione in base al '|'
            Uri? uri = string.IsNullOrEmpty(parts[2]) ? null : new Uri(parts[2]); //trasformazione parte 2 in uri
            return new Store(parts[0], parts[1], uri);
        }

        public Store[] GetStores4() =>
            File
            .ReadAllLines(_pathFile)
            .Skip(1) //quante ne vuoi saltare, salta la prima
            .Where(line => !string.IsNullOrEmpty(line)) //esclude le righe vuote //con il where bisogna ritornare un bool
            .Select//mappare da una sequenza a un'altra, nel nostro caso traforma delle stringhe in un'ogetto store
            (
                line =>
                {
                    string[] parts = line.Split('|', StringSplitOptions.TrimEntries); //divisione in base al '|'
                    Uri? uri = string.IsNullOrEmpty(parts[2]) ? null : new Uri(parts[2]); //trasformazione parte 2 in uri
                    return new Store(parts[0], parts[1], uri);
                }
            )
            .ToArray();
    }
}
