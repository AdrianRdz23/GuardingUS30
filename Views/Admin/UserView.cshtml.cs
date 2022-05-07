using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardingUS30.Models;
using GuardingUS30.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuardingUS30.Views.Login
{
    public class UserViewModel : PageModel
    {
        private DBCtx Context { get; }
        public UserViewModel(DBCtx _context)
        {
            this.Context = _context;
        }




        public List<UserModel> Users { get; set; }


        /*
        public void OnGet()
        {
            AdminDAO adminDAO = new AdminDAO();

            Users = adminDAO.ReadUsers();
        }
        */
    }
}
