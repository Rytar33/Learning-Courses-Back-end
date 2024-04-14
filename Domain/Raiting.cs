namespace Domain;

public class Raiting : BaseEntity
{
    public Raiting(
        Guid idCourse,
        Guid idUser,
        short quantityScore,
        string? comment = null) 
    {
        IdCourse = idCourse;
        IdUser = idUser;
        QuantityScore = quantityScore;
        Comment = comment;
    }

    public Guid IdCourse { get; init; }

    protected Course Course { get; init; }

    public Guid IdUser { get; init; }

    protected User User { get; init; }

    public short QuantityScore { get; init; }

    public string? Comment { get; init; }
}
