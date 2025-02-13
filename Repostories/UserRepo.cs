using V._3._0.App_Data;
using V._3._0.Interfaces;
using V._3._0.Models;

namespace V._3._0.Repostories
{
    public class UserRepo : IUser
    {
        public readonly HospitalData db;
        private List<Signup> Users = new()
        {
            new Signup {Name="Saifi",Email="saifi2@gmail.com",Password="3829"},
            new Signup {Name="Agha",Email="Agha12@gmail.com",Password="1090"}
        };
        public Signup GetByUsernameAndPassword(string username, string Email, string password)
        {
            var user = Users.FirstOrDefault(u=>
            u.Name == username && u.Password ==password && u.Email == Email);

            return user;
        }
        public Signup? GetByGoogleId()
        {
            throw new NotImplementedException();
        }

        
    }
}
