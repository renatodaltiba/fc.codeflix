using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.SeedWork;

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
        if (String.IsNullOrWhiteSpace(Name))
        {
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        }

        if (Description == null)
        {
            throw new EntityValidationException($"{nameof(Description)} should not be null");
        }
        
        if(Name.Length < 3)
        {
            throw new EntityValidationException($"{nameof(Name)} should be greater than 3 characters");
        }
        
        if(Name.Length > 255)
        {
            throw new EntityValidationException($"{nameof(Name)} should be less than 255 characters");
        }
        
        if(Description.Length > 10_000)
        {
            throw new EntityValidationException($"{nameof(Description)} should be less than 10_000 characters");
        }
    }
    
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description ?? Description;
        Validate();
    }
}