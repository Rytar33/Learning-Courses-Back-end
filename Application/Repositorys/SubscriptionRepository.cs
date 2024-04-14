using Application.Interfaces.Repositorys;
using CourseDbContext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositorys;

public class SubscriptionRepository : ISubscriptionRepository
{
    public SubscriptionRepository()
        => _dbContext = new LearningCourseDataBaseContext();

    private LearningCourseDataBaseContext _dbContext;

    public Task<IEnumerable<Subscription>> GetAll()
        => Task.FromResult(_dbContext.Subscription.AsNoTracking().AsEnumerable());

    public async Task<Subscription> GetByIdAsync(Guid id)
        => await _dbContext.Subscription.FindAsync(id);

    public async Task AddAsync(Subscription subscription)
        => await _dbContext.Subscription.AddAsync(subscription);

    public Task Update(Subscription subscription)
        => Task.FromResult(_dbContext.Subscription.Update(subscription));

    public Task Delete(Subscription subscription)
        => Task.FromResult(_dbContext.Subscription.Remove(subscription));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
