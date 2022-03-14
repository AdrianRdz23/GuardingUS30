using GuardingUS30.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class UserModel
    {
        //id of the user
        
        public int iduser { get; set; }

        //name of the user
        [Required]
        [StringLength(50, MinimumLength =1)]
        [DisplayName("Name needs to be typed correctly")]
        public string name { get; set; }

        //password of the user
        [Required]
        [StringLength(30, MinimumLength = 5)]
        [DisplayName("password needs to be typed correctly")]
        public string password { get; set; }

        //email of the user
        [Required]
        [StringLength(50, MinimumLength = 7)]
        [DisplayName("email needs to be typed correctly")]
        public string email { get; set; }

        //Phone number of the user
        [Required]
        [StringLength(20, MinimumLength = 7)]
        [DisplayName("phone number needs to be typed correctly")]
        public string phonenumber { get; set; }

        //foreign key for the role table
        public int idrole { get; set; }

        //status of the user
        public bool status { get; set; }

        //date when the user has been created
        public DateTime startDate { get; set; }

        //date if the user has benn modified
        public DateTime modificationDate { get; set; }





        /*
        public bool Empty
        {
            get{ return (iduser == 0 && string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phonenumber) && idrole == 0 && status == false && startDate == null && modificationDate == null); }
        }
        */
    }
}
