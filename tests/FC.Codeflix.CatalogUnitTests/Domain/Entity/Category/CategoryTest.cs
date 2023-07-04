
using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
namespace FC.Codeflix.CatalogUnitTests.Domain.Entity.Category;

public class CategoryTest
{
    [Fact(DisplayName = "Instantiate Category")]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

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
        var validData = new
        {
            Name = "category name",
            Description = "category description",
        };
        
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
        var validData = new
        {
            Name = name,
            Description = "category description"
        };

        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(validData.Name!, validData.Description));
        exception.Message.Should().Be($"{nameof(validData.Name)} should not be empty or null");
    } 
    
    [Fact(DisplayName = "Throw when description is empty")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category("category name", null!));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Description)} should not be null");
    }
    
    [Theory(DisplayName = "Throw when name is minor than 3 characters")]
    [InlineData("a")]
    [InlineData("ab")]
    public void ThrowWhenNameIsMinorThan3Characters(string name)
    {
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(name, "category description"));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Name)} should be greater than 3 characters");
    }
    
    [Fact(DisplayName = "Throw when name is major than 255 characters")]
    public void ThrowWhenNameIsMajorThan255Characters()
    {
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category(new string('a', 256), "category description"));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Name)} should be less than 255 characters");
    }
    
    [Fact(DisplayName = "Throw when description is major than 10_000 characters")]
    public void ThrowWhenDescriptionIsMajorThan10000Characters()
    {
        var exception = Assert.Throws<EntityValidationException>(() => new DomainEntity.Category("category name", new string('a', 10_001)));
        exception.Message.Should().Be($"{nameof(DomainEntity.Category.Description)} should be less than 10_000 characters");
    }
    
    [Fact(DisplayName = "It should activate category")]
    public void Activate()
    {
        var category = new DomainEntity.Category("category name", "category description", false);
        category.Activate();
        category.IsActive.Should().BeTrue();
    }
    
    [Fact(DisplayName = "Should deactivate category")]
    public void Deactivate()
    {
        var category = new DomainEntity.Category("category name", "category description", true);
        category.Deactivate();
        category.IsActive.Should().BeFalse();
    }
    
    [Fact(DisplayName = "should update category")]
    public void Update()
    {
        var category = new DomainEntity.Category("category name", "category description", true);
        var validData = new
        {
            Name = "category name updated",
            Description = "category description updated"
        };
        category.Update(validData.Name, validData.Description);
        
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
    }
}