using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class NotificationsModel
    {

        [Key]
        public int idnotification { get; set; }

        public string title { get; set; }

        public int iduser { get; set; }

        public string message { get; set; }

        public bool IsSelected { get; set; }

        //status of the user
        public byte status { get; set; }

        //date when the user has been created
        public DateTime startDate { get; set; }

        //date if the user has benn modified
        public DateTime modificationDate { get; set; }

    }
}
