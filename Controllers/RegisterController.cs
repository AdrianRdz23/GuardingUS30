using GuardingUS30.Models;
using GuardingUS30.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Controllers
{
    public class RegisterController : Controller
    {
        //Register page
        public IActionResult Register()
        {
            return View();
        }

        //Method to be able to register a user
        public IActionResult ProcessRegister(UserModel userModel)
        {
            //You create a new userDao to be able to connect with the database
            UsersDAO usersDAO = new UsersDAO();

            //Apply the method to insert a user on the database
            usersDAO.InsertUser(userModel);

            //You will go to the register Success
            return View("RegisterSuccess", userModel);
         


        }
    }
    }
