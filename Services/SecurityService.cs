using GuardingUS30.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Services
{
    public class SecurityService
    {
        //You create a new userDao to be able to connect with the database
        UsersDAO usersDAO = new UsersDAO();

        //Check if the user that you used to log in is valid or not
        /*
        public bool IsValid(UserModel user)
        {
            return usersDAO.FindUserByNameandPassword(user);

        }
        */
    }
}
