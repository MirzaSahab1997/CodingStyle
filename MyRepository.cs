public class MyRepositories
{
    private readonly MyDbContext _myDbContext;

    private UserRepository _userRepository;

    public CaaryRepositories(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext ?? throw new ArgumentNullException($"{nameof(CaaryRepositories)}");
    }

    public UserRepository UserRepository => _userRepository ?? (_userRepository = CreateUserRepository());

    private UserRepository CreateUserRepository()
    {
        return new UserRepository(_myDbContext);
    }
}
