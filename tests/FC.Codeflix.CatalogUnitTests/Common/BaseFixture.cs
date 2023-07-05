using Bogus;

namespace FC.Codeflix.CatalogUnitTests.Common;

public abstract class BaseFixture
{
    protected BaseFixture()
    {
        Faker = new Faker("pt_BR");
    }

    public Faker Faker { get; set; }
}