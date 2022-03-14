using Microsoft.EntityFrameworkCore;
using GuardingUS30.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GuardingUS30.Services
{
    public class DBCtx : DbContext
    {

        public DBCtx(DbContextOptions<DBCtx> options) : base(options)
        {

        }

        public DbSet<HomeModel> Home {get; set;}

        public DbSet<VisitorModel> Visitor {get; set;}

        


       //public DbSet<UserModel> Users { get; set; }
       
       //public DbSet<RoleModel> Roles { get; set; }

       
    }
}
