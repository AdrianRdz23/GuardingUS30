using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class VisitorModel
    {
        [Key]
        public int idvisitor { get; set; }

        public string name { get; set; }

        public string carPlate { get; set; }

        public DateTime entrance { get; set; }

        public DateTime exit { get; set; }

        public string identification { get; set; }

        public int idhome { get; set; }

        public string description { get; set; }


        //status of the user
        public bool status { get; set; }

        //date when the user has been created
        public DateTime startDate { get; set; }

        //date if the user has benn modified
        public DateTime modificationDate { get; set; }
    }
}
