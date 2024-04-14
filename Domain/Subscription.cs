namespace Domain;

public class Subscription : BaseEntity
{
    public Subscription(
        Guid idUser,
        Guid idCourse,
        DateTime dateTimePayment,
        DateTime dateTimeEndSubscription) 
    {
        IdUser = idUser;
        IdCourse = idCourse;
        DateTimePayment = dateTimePayment;
        DateTimeEndSubscription = dateTimeEndSubscription;
    }

    public Guid IdUser { get; init; }

    protected User User { get; init; }

    public Guid IdCourse { get; init; }

    protected Course Course { get; init; }

    public DateTime DateTimePayment { get; init; }

    public DateTime DateTimeEndSubscription { get; init; }
}
