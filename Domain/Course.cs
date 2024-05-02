using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validations;
using Domain.Validations.Validators;

namespace Domain;

[Table("course")]
public class Course : BaseEntity
{
    public Course(
        string name,
        string description,
        Guid? idUser) 
    {
        Name = name;
        Description = description;
        IdUser = idUser;
        new CourseValidator().ValidateWithErrors(this);
    }

    [Column("name")]
    public string Name { get; private set; }

    [Column("description")]
    public string Description { get; private set; }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid? IdUser { get; private set; }

    public User? User { get; private init; }

    [Column("is_active")]
    public bool IsActive { get; private set; } = false;
    
    public Course Update(
        string? name = null,
        string? description = null,
        Guid? idUser = null,
        bool? isActive = null)
    {
        if (name != null)
            Name = name;
        if (description != null)
            Description = description;
        if (idUser != null)
            IdUser = idUser;
        if (isActive != null)
            IsActive = isActive.Value;
        new CourseValidator().ValidateWithErrors(this);
        return this;
    }
}
