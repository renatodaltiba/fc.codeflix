using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Validation;

public class DomainValidation
{
    public static void NotNull(object target, string fieldName)
    {
        if (target is null)
            throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be null or empty");
    }
    
    public static void MinLength(string? target, string fieldName, int minLength)
    {
        if (target?.Length < minLength)
            throw new EntityValidationException($"{fieldName} should have at least {minLength} characters");
    }
    
    public static void MaxLength(string? target, string fieldName, int maxLength)
    {
        if (target?.Length > maxLength)
            throw new EntityValidationException($"{fieldName} should have at most {maxLength} characters");
    }
}