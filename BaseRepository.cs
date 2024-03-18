public class BaseRepository
{
    protected readonly MyDbContext _myDbContext;

    public BaseRepository(MyDbContext myDbContext)
        => _myDbContext = myDbContext ?? throw new ArgumentNullException($"{nameof(myDbContext)}");

    public async Task<(T, bool)> AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class
    {
        _myDbContext.Set<T>().AddRange(entity);

        var isAdded = await _myDbContext.SaveChangesAsync(cancellationToken) > 0;

        return (entity, isAdded);
    }

    public async Task<(IEnumerable<T>, bool)> AddEntityAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        where T : class
    {
        _myDbContext.Set<T>().AddRange(entities);

        var isAdded = await _myDbContext.SaveChangesAsync(cancellationToken) > 0;

        return (entities, isAdded);
    }

    public async Task<(T, bool)> UpdateEntityAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class
    {
        _myDbContext.Entry(entity).State = EntityState.Modified;

        var isUpdated = await _myDbContext.SaveChangesAsync(cancellationToken) > 0;

        return (entity, isUpdated);
    }

    public async Task<(IEnumerable<T>, bool)> UpdateEntityAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        where T : class
    {
        foreach (T entity in entities)
        {
            _myDbContext.Entry(entity).State = EntityState.Modified;
        }

        var isUpdated = await _myDbContext.SaveChangesAsync(cancellationToken) > 0;

        return (entities, isUpdated);
    }

    public async Task<(T, bool)> DeleteEntityAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class
    {
        _myDbContext.Set<T>().Remove(entity);

        var isDeleted = await _myDbContext.SaveChangesAsync(cancellationToken) > 0;

        return (entity, isDeleted);
    }

    public async Task<(IEnumerable<T>, bool)> DeleteEntityAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        where T : class
    {
        foreach (T entity in entities)
        {
            _myDbContext.Entry(entity).State = EntityState.Deleted;
        }

        var isDeleted = await _myDbContext.SaveChangesAsync(cancellationToken) > 0;

        return (entities, isDeleted);
    }
}
