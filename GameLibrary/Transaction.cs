namespace GameLibrary
{
    public class Transaction
    {
        
        public DateTime PurchaseDate { get; }
        public bool IsVirtual { get; }
        public Store Store { get; }
        public Game Game { get; }
        public Platform Platform { get; }
        public decimal Price { get; }
        public CurrencyType Currency { get; } 
        public PaymentMethod PaymentMethod { get; }

        public Transaction(DateTime purchaseDate, Game game, Store store, Platform platform, decimal price, CurrencyType currency = CurrencyType.Eur, PaymentMethod paymentMethod = PaymentMethod.Card, bool isVirtual = true)
        {
            PurchaseDate = purchaseDate;
            IsVirtual = isVirtual;
            Game = game ?? throw new ArgumentNullException(nameof(game));
            Store = store ?? throw new ArgumentNullException(nameof(store));
            Platform = platform ?? throw new ArgumentNullException(nameof(platform));
            Price = price;
            Currency = currency;
            PaymentMethod = paymentMethod;
        }
    }
}
