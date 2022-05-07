using GuardingUS30.Models;
using GuardingUS30.Services;
using GuardingUS30.Views.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace GuardingUS30.Controllers
{
    public class LoginController : Controller
    {

        private readonly DBCtx _context;

        public LoginController(DBCtx context)
        {
            _context = context;
        }
        //Public method to go to the Login Form
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SecurityGuardView()
        {
            return View();
        }

       

        //A method to check if the User logged in correctly
        public IActionResult ProcessLogin(UserModel userModel)
        {

            UsersDAO usersDAO = new UsersDAO();

            //if statement to know if the User exists or not. If the user is inside of the database or not.
            UserModel testuser = usersDAO.FindUserByNameandPassword(userModel);

            UserNotificationModel notuser = new UserNotificationModel();
            notuser.user = testuser;

            if (testuser.idrole == 1)
            {
                UserNotificationModel user = usersDAO.FindUserByNameandPasswordBeta(notuser);

                if(user.mymessages == 0)
                {
                    user = usersDAO.FindUserByNameandPasswordNoMessages(notuser); 
                }


                return View("ResidentView", user);
            }
            else
            {

                switch (testuser.idrole)
                {
                    case 2:
                        return View("AdminView", testuser);

                    case 3:
                        return View("SecurityGuardView", testuser);

                    default:
                        return View("LoginFailure", testuser);

                }
            }

        }





        //Action to logout
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }


    }
}
