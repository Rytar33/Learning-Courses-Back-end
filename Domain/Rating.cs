using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validations.Validators;

namespace Domain;

[Table("rating")]
public class Rating : BaseEntity
{
    public Rating(
        Guid idCourse,
        Guid idUser,
        int quantityScore,
        string? comment = null) 
    {
        IdCourse = idCourse;
        IdUser = idUser;
        QuantityScore = quantityScore;
        Comment = comment;
        new RatingValidator().Validate(this);
    }

    [Column("id_course")]
    [ForeignKey(nameof(Course))]
    public Guid IdCourse { get; init; }

    public Course Course { get; private init; }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid IdUser { get; init; }

    public User User { get; private init; }

    [Column("quantity_score")]
    public int QuantityScore { get; init; }

    [Column("comment")]
    public string? Comment { get; init; }
}
