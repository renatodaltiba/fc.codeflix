using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.CatalogUnitTests.Domain.Entity.Category;

public class CategoryTestFixture
{
    public DomainEntity.Category ValidCategory => new("category name", "category description");
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> {}