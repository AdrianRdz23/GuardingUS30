using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class HomeModel
    {
        [Key]
        public int idhome { get; set; }

        public int number { get; set; }

        public int cars { get; set; }

        public string address { get; set; }

        public int iduser { get; set; }


        //status of the user
        public byte status { get; set; }

        //date when the user has been created
        public DateTime startDate { get; set; }

        //date if the user has benn modified
        public DateTime modificationDate { get; set; }
    }
}
