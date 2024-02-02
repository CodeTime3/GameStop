using GameLibrary;
using GameLibrary.Import.Txt;

namespace GameStopTestSuite
{
    public class TestGameLibrary
    {

        [Theory]
        [InlineData("C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileStores.txt")]
        public void TextFileStoreImporters_shuld_work2(string filePath)
        {
            var storeImporter = new TextFileStoreImporter(filePath);

            var result = storeImporter.GetStores3();

            Assert.NotNull(result);
            Assert.IsType<Store[]>(result);

            var steamStore = result[0];
            Assert.Equal("Steam", steamStore.Name);
        }



        [Theory]
        [InlineData("C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileGames.txt")]
        public void TextFileGamesImporters_shuld_work(string filePath)
        {

            var gameImporter = new TextFileGameImporter(filePath);

            var result = gameImporter.GetGames();

            Assert.NotNull(result);
            Assert.IsType<Game[]>(result);
        }

        [Theory]
        [InlineData
            (
            "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileTransaction.txt",
            "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileGames.txt",
            "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileStores.txt",
            "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FilePlatform.txt"
            )]
        public void TextFileTransactionImporters_shuld_work(string fileTransactions, string fileGames, string fileStores, string filePlatforms)
        {
            var gameImporter = new TextFileGameImporter(fileGames);

            var gameCollection = gameImporter.GetGames();

            var storeImporter = new TextFileStoreImporter(fileStores);

            var storeCollection = storeImporter.GetStores();

            var platformImporter = new TextFilePlatformImporter(filePlatforms);

            var platformCollection = platformImporter.GetPlatforms();

            var transactionImporter = new TextFileTransactionImporter(fileTransactions, storeCollection, gameCollection, platformCollection);

            var result = transactionImporter.GetTransactions();

            Assert.NotNull(result);
            Assert.IsType<Transaction[]>(result);
            Assert.True(result.Length == 2);
            Assert.Equal("Steam", result[0].Store.Name);
            Assert.Equal(new DateTime(2012, 06, 01), result[1].PurchaseDate);
        }

        [Theory]
        [InlineData
           (
           "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileTransaction.txt",
           "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileGames.txt",
           "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FileStores.txt",
           "C:\\Users\\danie\\source\\repos\\GameStop\\GameLibrary.Import.Txt\\FilePlatform.txt"

           )]
        public void ImportAllGamesData_shuld_work(string fileTransactions, string fileGames, string fileStores, string filePlatforms)
        {

            var importAllGames = new ImportAllGamesData(fileGames, fileStores, filePlatforms, fileTransactions);
            var (games, stores, platforms, transactions) = importAllGames.ImportAll();
            var allData = importAllGames.ImportAll();

            Assert.NotNull(transactions);
            Assert.IsType<Transaction[]>(transactions);
            Assert.True(transactions.Length == 2);
            Assert.Equal("Steam", transactions[0].Store.Name);
            Assert.Equal(new DateTime(2012, 06, 01), transactions[1].PurchaseDate);
            Assert.Equal("Console", platforms[1].Description);
        }
    }
}
