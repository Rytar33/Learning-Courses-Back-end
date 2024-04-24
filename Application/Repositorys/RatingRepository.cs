using Application.Interfaces.Repositorys;
using CourseDbContext;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositorys;

public class RatingRepository : IRatingRepository
{
    public RatingRepository()
        => _dbContext = new LearningCourseDataBaseContext();

    private LearningCourseDataBaseContext _dbContext;

    public Task<IEnumerable<Rating>> GetAll()
        => Task.FromResult(_dbContext.Rating.AsNoTracking().AsEnumerable());

    public async Task<Rating> GetByIdAsync(Guid id)
        => await _dbContext.Rating.FindAsync(id);

    public async Task AddAsync(Rating rating)
        => await _dbContext.Rating.AddAsync(rating);

    public Task Update(Rating rating)
        => Task.FromResult(_dbContext.Rating.Update(rating));

    public Task Delete(Rating rating)
        => Task.FromResult(_dbContext.Rating.Remove(rating));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
