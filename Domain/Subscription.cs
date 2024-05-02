using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validations;
using Domain.Validations.Validators;
using FluentValidation;

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
        new SubscriptionValidator().ValidateWithErrors(this);
    }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public Guid IdUser { get; private set; }

    public User User { get; private init; }

    [Column("id_course")]
    [ForeignKey(nameof(Course))]
    public Guid IdCourse { get; private set; }

    public Course Course { get; private init; }

    [Column("date_time_payment")]
    public DateTime DateTimePayment { get; init; }

    [Column("date_time_subscription")]
    public DateTime DateTimeEndSubscription { get; private set; }

    public Subscription Update(
        Guid? idUser = null,
        Guid? idCourse = null,
        DateTime? dateTimeEndSubscription = null)
    {
        if (idUser != null)
            IdUser = idUser.Value;
        if (idCourse != null)
            IdCourse = idCourse.Value;
        if (dateTimeEndSubscription != null)
        {
            if (dateTimeEndSubscription < DateTimeEndSubscription)
                throw new ValidationException("Время окончание подписки не должно быть раньше текущей");
            DateTimeEndSubscription = dateTimeEndSubscription.Value;
        }
        new SubscriptionValidator().ValidateWithErrors(this);
        return this;
    }
}
