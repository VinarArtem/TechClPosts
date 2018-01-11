using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechClPosts.Models.AppModels;

namespace TechClPosts.Models.RepoInterfaces
{
    interface IUsersRepository
    {
        void AddUser(User user);

        User GetUser(Guid userKey);

        IEnumerable<User> AllUsers();

        void EditUser(User updatedUser);

        void DeleteUser(User userToDelete);

        User UserLogin(string userName, string userPassword);
    }
}
