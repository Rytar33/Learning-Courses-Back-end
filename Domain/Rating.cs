using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validations;
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
        new RatingValidator().ValidateWithErrors(this);
    }

    [Column("id_course")]
    [ForeignKey(nameof(Course))]
    public Guid IdCourse { get; private set; }

    public Course Course { get; private init; }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid IdUser { get; private set; }

    public User User { get; private init; }

    [Column("quantity_score")]
    public int QuantityScore { get; private set; }

    [Column("comment")]
    public string? Comment { get; private set; }
    
    public Rating Update(
        Guid? idCourse = null,
        Guid? idUser = null,
        int? quantityScore = null,
        string? comment = null)
    {
        if (idCourse != null)
            IdCourse = idCourse.Value;
        if (idUser != null)
            IdUser = idUser.Value;
        if (quantityScore != null)
            QuantityScore = quantityScore.Value;
        if (comment != null)
            Comment = comment;
        new RatingValidator().ValidateWithErrors(this);
        return this;
    }
}
