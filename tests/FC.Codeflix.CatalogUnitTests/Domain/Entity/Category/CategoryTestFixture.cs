using FC.Codeflix.CatalogUnitTests.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.CatalogUnitTests.Domain.Entity.Category;

public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture()
        : base()
    {
        
    }
    public DomainEntity.Category ValidCategory => new(Faker.Lorem.Sentence(1), Faker.Lorem.Text());
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> {}