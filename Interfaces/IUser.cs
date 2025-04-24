using V._3._0.Models;

namespace V._3._0.Interfaces
{
    public interface IUser
    {
        public Signup? GetByUsernameAndPassword(string username,string Email, string password);
        public Signup? GetByGoogleId();
    }
}
