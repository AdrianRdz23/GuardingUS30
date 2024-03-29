﻿using GuardingUS30.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class RoleModel
    {
        public int idrole { get; set; }

        public string name { get; set; }


        //status of the user
        public bool status { get; set; }

        //date when the user has been created
        public DateTime startDate { get; set; }

        //date if the user has benn modified
        public DateTime modificationDate { get; set; }

        

    }
}
