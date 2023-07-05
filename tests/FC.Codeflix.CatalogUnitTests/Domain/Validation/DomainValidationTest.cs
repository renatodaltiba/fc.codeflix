using Bogus;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.Codeflix.CatalogUnitTests.Domain.Validation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();
    
    [Fact(DisplayName = "Not Null")]
    [Trait("Domain", "DomainValidation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();

        var action = () => DomainValidation.NotNull(value, "Value");

        action.Should().NotThrow();
    }
    
    [Fact(DisplayName = "Not Null with null value")]
    [Trait("Domain", "DomainValidation")]
    public void NotNullWithNullValue()
    {
        var action = () => DomainValidation.NotNull(null!, "FieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null");
    }

    [Theory(DisplayName = "Not Null or Empty")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData(null)]
    public void NotNullOrEmptyThrowWhenEmpry(string? target)
    {
        var action = () => DomainValidation.NotNullOrEmpty(target, "Target");
        
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Target should not be null or empty");
    }
    
    [Fact(DisplayName = "Not Null or Empty OK")]
    public void NotNullOrEmptyOk()
    {
        var value = Faker.Commerce.ProductName();

        var action = () => DomainValidation.NotNullOrEmpty(value, "Value");

        action.Should().NotThrow();
    }
    
    [Fact(DisplayName = "Min Length")]
    public void MinLengthOk()
    {
        var value = Faker.Commerce.ProductName();

        var action = () => DomainValidation.MinLength(value, "Value", 5);

        action.Should().NotThrow();
    }
    
    [Theory(DisplayName = "Min Length Throw")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("1234")]
    public void MinLengthThrow(string? target)
    {
        var action = () => DomainValidation.MinLength(target, "Target", 5);
        
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Target should have at least 5 characters");
    }
    
    [Fact(DisplayName = "Max Length Ok")]
    public void MaxLengthOk()
    {

        var action = () => DomainValidation.MaxLength("1234", "Value", 5);

        action.Should().NotThrow();
    }
    
    [Theory(DisplayName = "Max Length Throw")]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void MaxLengthThrow(string? target)
    {
        var action = () => DomainValidation.MaxLength(target, "Target", 5);
        
        action.Should().Throw<EntityValidationException>()
            .WithMessage($"Target should have at most 5 characters");
    }
}