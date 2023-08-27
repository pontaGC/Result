namespace Results.Test.Results.Mocks
{
    internal class MockResultValue
    {
        public MockResultValue(int number, string name, IEnumerable<string> strings)
        {
            this.Number = number;
            this.Name = name;
            this.Strings = strings.ToList();
        }

        public MockResultValue(int number, string name)
        {
            this.Number = number;
            this.Name = name;
            this.Strings = new List<string>();
        }

        public MockResultValue(IEnumerable<string> strings)
            : this (DefaultNumber, DefaultName, strings)
        {
        }

        public MockResultValue()
            : this(DefaultNumber, DefaultName, Enumerable.Empty<string>())
        {
        }

        internal const int DefaultNumber = 1;

        internal const string DefaultName = "MockResult";

        public int Number { get; }

        public string Name { get; }

        public List<string> Strings { get; }
    }
}
