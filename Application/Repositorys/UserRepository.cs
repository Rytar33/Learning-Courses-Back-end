using Application.Interfaces.Repositorys;
using CourseDbContext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositorys;

public class UserRepository : IUserRepository
{
    public UserRepository() 
        => _dbContext = new LearningCourseDataBaseContext();

    private LearningCourseDataBaseContext _dbContext;

    public Task<IEnumerable<User>> GetAll()
        => Task.FromResult(_dbContext.User.AsNoTracking().AsEnumerable());

    public async Task<User> GetByIdAsync(Guid id)
        => await _dbContext.User.FindAsync(id);

    public async Task AddAsync(User user)
        => await _dbContext.User.AddAsync(user);

    public Task Update(User user)
        => Task.FromResult(_dbContext.User.Update(user));

    public Task Delete(User user)
        => Task.FromResult(_dbContext.User.Remove(user));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
