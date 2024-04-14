using System.ComponentModel.DataAnnotations.Schema;

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
    }

    [Column("name")]
    public string Name { get; init; }

    [Column("description")]
    public string Description { get; init; }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid? IdUser { get; init; }

    public User? User { get; private init; }
}
