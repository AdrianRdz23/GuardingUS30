using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class UserNotificationModel
    {

        //Create a visitor Model
        public UserModel user { get; set; }

        //Create a home Model
        public NotificationsModel notification { get; set; }

        public string mymessages { get; set; }

        public int status { get; set; }

    }
}
