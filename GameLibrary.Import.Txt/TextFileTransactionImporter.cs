using System.IO;
using System;
using System.Data;

namespace GameLibrary.Import.Txt
{
    public class TextFileTransactionImporter
    {
        private readonly string _fileName;
        private readonly Store[] _stores;
        private readonly Game[] _games;
        private readonly Platform[] _platform;

        public TextFileTransactionImporter(string fileName, Store[] stores, Game[] games, Platform[] platforms)
        {
            _fileName = fileName;
            _stores = stores;
            _games = games;
            _platform = platforms;
        }

        public Transaction[] GetTransactions() =>
            File
            .ReadAllLines( _fileName )
            .Skip( 1 )
            .Where(line => !string.IsNullOrEmpty(line))
            .Distinct()
            .Select(BuildTransactionFromLine)
            .ToArray();

        private Transaction BuildTransactionFromLine( string line )
        {
            string[] parts = line.Split('|', StringSplitOptions.TrimEntries);
            DateTime purchaseDate = ParseDate(parts[0]);
            bool isVirtual = ParseBool(parts[1]);

            Store? store = GetStoreFromName(parts[2]);
            if (store is null) 
            {
                throw new Exception($"The store {parts[2]} does not exist");
                    
            }

            Game? game = GetGameFromName(parts[3]);
            if (game is null)
            {
                throw new Exception($"The game {parts[3]} does not exist");
            }

            Platform? platform = GetPlatformFromName(parts[4]);
            if (platform is null)
            {
                throw new Exception($"The platform {parts[4]} does not exist");
            }

            decimal price = ParseDecimal(parts[5]);

            CurrencyType currencyType = GetCurrencyType(parts[6]);
            PaymentMethod paymentMethod = GetPaymentMethod(parts[7]);

            return new Transaction(purchaseDate, game, store, platform, price, currencyType, paymentMethod, isVirtual);
        }

        private static DateTime ParseDate( string line ) =>
            DateTime.ParseExact(line, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

        private static bool ParseBool ( string line ) => bool.Parse(line);

        private Store? GetStoreFromName_classic( string name )
        {
            foreach (var store in _stores)
            {
                if (string.Equals(store.Name, name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return store;
                }
            }

            return null;
        }

        private Store? GetStoreFromName(string name) => 
            _stores
            .FirstOrDefault(s => string.Equals( s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        private Game? GetGameFromName(string name) =>
            _games
            .FirstOrDefault(g => string.Equals( g.Name, name, StringComparison.CurrentCultureIgnoreCase));

        private Platform? GetPlatformFromName(string name) =>
            _platform
            .FirstOrDefault(p => string.Equals( p.Name, name, StringComparison.CurrentCultureIgnoreCase));

        private static decimal ParseDecimal(string line) => 
            decimal.Parse(line, System.Globalization.CultureInfo.InvariantCulture);

        private CurrencyType GetCurrencyType(string currency) =>
            (CurrencyType)Enum.Parse(typeof(CurrencyType), currency);

        private PaymentMethod GetPaymentMethod(string paymentMethod) =>
            (PaymentMethod)Enum.Parse(typeof(PaymentMethod), paymentMethod);


    }
}
