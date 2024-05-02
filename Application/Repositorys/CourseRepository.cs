namespace Application.Repositorys;

public class CourseRepository : ICourseRepository
{
    public CourseRepository()
        => _dbContext = new LearningCourseDataBaseContext();

    private LearningCourseDataBaseContext _dbContext;

    public Task<IEnumerable<Course>> GetAll()
        => Task.FromResult(_dbContext.Course.AsNoTracking().AsEnumerable());

    public async Task<Course?> GetByIdAsync(Guid id)
        => await _dbContext.Course.FindAsync(id);

    public async Task AddAsync(Course course)
        => await _dbContext.Course.AddAsync(course);

    public Task Update(Course course)
        => Task.FromResult(_dbContext.Course.Update(course));

    public Task Delete(Course course)
        => Task.FromResult(_dbContext.Course.Remove(course));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
