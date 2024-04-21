using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validations.Validators;

namespace Domain;

[Table("subscription")]
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
        new SubscriptionValidator().Validate(this);
    }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid IdUser { get; init; }

    public User User { get; private init; }

    [Column("id_course")]
    [ForeignKey(nameof(Course))]
    public Guid IdCourse { get; init; }

    public Course Course { get; private init; }

    [Column("date_time_payment")]
    public DateTime DateTimePayment { get; init; }

    [Column("date_time_subscription")]
    public DateTime DateTimeEndSubscription { get; init; }
}
