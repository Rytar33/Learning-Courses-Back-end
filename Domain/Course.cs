using System.ComponentModel.DataAnnotations.Schema;
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
        new CourseValidator().Validate(this);
    }

    [Column("name")]
    public string Name { get; private set; }

    [Column("description")]
    public string Description { get; private set; }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid? IdUser { get; private set; }

    public User? User { get; private init; }

    public Course Update(
        string? name = null,
        string? description = null,
        Guid? idUser = null)
    {
        if (name != null)
            Name = name;
        if (description != null)
            Description = description;
        if (idUser != null)
            IdUser = idUser;
        new CourseValidator().Validate(this);
        return this;
    }
}
