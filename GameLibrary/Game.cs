namespace GameLibrary
{
    public class Game
    {
        public string Name { get; }
        public string Description { get; }
        public string[] Tags { get; }

        public Game() { }

        public Game(string name, string description, string[] tags)
        {
            Name = name;
            Description = description;
            Tags = tags;
        }
    }
}
