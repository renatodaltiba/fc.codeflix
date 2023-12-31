using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.SeedWork;
using FC.Codeflix.Catalog.Domain.Validation;

namespace FC.Codeflix.Catalog.Domain.Entity;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public Category(string name, string description, bool isActive = true)
        : base()
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        
        Validate();
    }

    public void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, nameof(Name), 3);
        DomainValidation.MaxLength(Name, nameof(Name), 255);
        DomainValidation.MaxLength(Description, nameof(Description), 10_000);
        DomainValidation.NotNull(Description, nameof(Description));
    }
    
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description ?? Description;
        Validate();
    }
}