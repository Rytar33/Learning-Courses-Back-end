using Application.Interfaces.Repositorys;
using CourseDbContext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositorys;

public class RaitingRepository : IRaitingRepository
{
    public RaitingRepository()
        => _dbContext = new LearningCourseDataBaseContext();

    private LearningCourseDataBaseContext _dbContext;

    public Task<IEnumerable<Raiting>> GetAll()
        => Task.FromResult(_dbContext.Raiting.AsNoTracking().AsEnumerable());

    public async Task<Raiting> GetByIdAsync(Guid id)
        => await _dbContext.Raiting.FindAsync(id);

    public async Task AddAsync(Raiting raiting)
        => await _dbContext.Raiting.AddAsync(raiting);

    public Task Update(Raiting raiting)
        => Task.FromResult(_dbContext.Raiting.Update(raiting));

    public Task Delete(Raiting raiting)
        => Task.FromResult(_dbContext.Raiting.Remove(raiting));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
