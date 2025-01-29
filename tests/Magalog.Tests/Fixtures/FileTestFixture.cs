using Bogus;
using System.Text;

namespace Magalog.Tests.Fixtures;

[CollectionDefinition(nameof(FileCollection))]
public class FileCollection : ICollectionFixture<FileTestFixture>{ }

public class FileTestFixture : IDisposable
{
    public StringBuilder GenerateLines(int quantity)
    {
        var faker = new Faker();
        var lines = new StringBuilder();

        for (int i = 0; i < quantity; i++)
        {
            lines.AppendLine(faker.Random.Int(1, 9999999).ToString("D10") +
                          faker.Name.FullName().PadLeft(45).Substring(0, 45) + 
                          faker.Random.Int(1, 9999999).ToString("D10") + 
                          faker.Random.Int(1, 9999999).ToString("D10") +
                          faker.Finance.Amount(1000, 2000, 2).ToString("F2").PadLeft(12) +
                          faker.Date.Past(5).ToString("yyyyMMdd")
            );
        }

        return lines;
    }

    public void Dispose()
    {        
    }
}