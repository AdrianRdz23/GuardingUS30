using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class UserRoleModel
    {

        // single instance of a user - adrian
        public UserModel userModel { get; set; }

        // want a list of roles availabe
        public List<String[]> roleModelNames { get; set; }

        public RoleModel roleModel { get; set; }

       

        public UserRoleModel(UserModel userModel, List<String[]> roleModelNames)
        {
            this.userModel = userModel;
            this.roleModelNames = roleModelNames;
        }

        public UserRoleModel()
        {

        }
        // getting - single role
        //  public RoleModel roleModel { get; set; }

        // public SelectList rolelist { get; set; }


    }
}
