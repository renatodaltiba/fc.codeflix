
using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
namespace FC.Codeflix.CatalogUnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _fixture;

    public CategoryTest(CategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Instantiate Category")]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validData = _fixture.ValidCategory;

        var datetimeBefore = DateTime.Now;
        
        var category = new DomainEntity.Category(validData.Name, validData.Description);
        
        var datetimeAfter = DateTime.Now;

        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().BeAfter(datetimeBefore);
        category.CreatedAt.Should().BeBefore(datetimeAfter);
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = "Instantiate Category with invalid data")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActiveStatus(bool isActive)
    {
        var validData = _fixture.ValidCategory;
        
        var datetimeBefore = DateTime.Now;
        
        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
        
        var datetimeAfter = DateTime.Now;


        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().BeAfter(datetimeBefore);
        category.CreatedAt.Should().BeBefore(datetimeAfter);
        category.IsActive.Should().Be(isActive);
    }

    [Theory(DisplayName = "Throw when name is empty")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ThrowWhenNameIsEmpty(string? name)
    {
        var validData = _fixture.ValidCategory;
        
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(name!, validData.Description));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Name)} should not be null or empty");
    } 
    
    [Fact(DisplayName = "Throw when description is empty")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var validData = _fixture.ValidCategory;

        
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(validData.Name, null!));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Description)} should not be null");
    }
    
    [Theory(DisplayName = "Throw when name is minor than 3 characters")]
    [InlineData("a")]
    [InlineData("ab")]
    public void ThrowWhenNameIsMinorThan3Characters(string name)
    {
        var validData = _fixture.ValidCategory;
        
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(name, validData.Description));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Name)} should have at least 3 characters");
    }
    
    [Fact(DisplayName = "Throw when name is major than 255 characters")]
    public void ThrowWhenNameIsMajorThan255Characters()
    {
        var validData = _fixture.ValidCategory;

        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(new string('a', 256), validData.Description));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Name)} should have at most 255 characters");
    }
    
    [Fact(DisplayName = "Throw when description is major than 10_000 characters")]
    public void ThrowWhenDescriptionIsMajorThan10000Characters()
    {
        
        var validData = _fixture.ValidCategory;

        
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(validData.Name, new string('a', 10_001)));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Description)} should have at most 10000 characters");
    }
    
    [Fact(DisplayName = "It should activate category")]
    public void Activate()
    {
        var validData = _fixture.ValidCategory;

        var category = new DomainEntity.Category(validData.Name, validData.Description, false);
        category.Activate();
        category.IsActive.Should().BeTrue();
    }
    
    [Fact(DisplayName = "Should deactivate category")]
    public void Deactivate()
    {
        var validData = _fixture.ValidCategory;

        var category = new DomainEntity.Category(validData.Name, validData.Description, true);
        category.Deactivate();
        category.IsActive.Should().BeFalse();
    }
    
    [Fact(DisplayName = "should update category")]
    public void Update()
    {
        var validData = _fixture.ValidCategory;

        
        var category = new DomainEntity.Category(validData.Name, validData.Description, true);
        var newValidData = new
        {
            Name = "category name updated",
            Description = "category description updated"
        };
        category.Update(newValidData.Name, newValidData.Description);
        
        category.Name.Should().Be(newValidData.Name);
        category.Description.Should().Be(newValidData.Description);
    }
}