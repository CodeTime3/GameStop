namespace GameLibrary
{
    public class Store
    {
        public string Name { get; }
        public string Description { get; }
        public Uri? StoreAddress { get; }

        public Store(string name, string description, Uri? storeAddress)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? name; //se description è null mette name 
            StoreAddress = storeAddress;
        }

        public Store() { }
    }
}
