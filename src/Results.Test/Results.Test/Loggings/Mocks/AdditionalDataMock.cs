namespace Results.Test.Loggings.Mocks
{
    internal class AdditionalDataMock
    {
        public AdditionalDataMock(string name = nameof(AdditionalDataMock))
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
