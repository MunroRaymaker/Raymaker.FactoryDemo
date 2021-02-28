namespace Raymaker.FactoryDemo.Console
{
    public interface IUserRepository
    {
        bool AddUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        public bool AddUser(User user)
        {
            return true;
        }
    }
}