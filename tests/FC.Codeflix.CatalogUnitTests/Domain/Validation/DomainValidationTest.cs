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
}