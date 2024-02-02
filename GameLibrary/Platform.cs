namespace GameLibrary
{
    public class Platform
    {
        public string Name { get; }
        public string Description { get; }

        public Platform(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? name;
        }
    }
}