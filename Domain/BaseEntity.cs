namespace Domain;

public abstract class BaseEntity
{
    public Guid Id { get; init; }

    public override bool Equals(object? obj)
        => obj != null 
        && obj is BaseEntity entity 
        && entity.Id == Id;

    public override int GetHashCode()
        => Id.GetHashCode();
}
