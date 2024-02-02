namespace GameLibrary.Import.Txt
{
    public class ImportAllGamesData
    {
        private readonly string _gameFilePath;
        private readonly string _storeFilePath;
        private readonly string _platformFilePath;
        private readonly string _transactionFilePath;

        public ImportAllGamesData(string gameFilePath, string storeFilePath, string platformFilePath, string transactionFilePath)
        {
            _gameFilePath = gameFilePath;
            _storeFilePath = storeFilePath;
            _platformFilePath = platformFilePath;
            _transactionFilePath = transactionFilePath;
        }

        public (Game[] Games, Store[] Stores, Platform[] Platforms, Transaction[] Transactions) ImportAll()
        {
            var gameImporter = new TextFileGameImporter(_gameFilePath);

            var gameCollection = gameImporter.GetGames();

            var storeImporter = new TextFileStoreImporter(_storeFilePath);

            var storeCollection = storeImporter.GetStores();

            var platformImporter = new TextFilePlatformImporter(_platformFilePath);

            var platformCollection = platformImporter.GetPlatforms();

            var transactionImporter = new TextFileTransactionImporter(_transactionFilePath,  storeCollection, gameCollection, platformCollection);

            var result = transactionImporter.GetTransactions();

            return (gameCollection, storeCollection, platformCollection, result);
        }
    }
}
