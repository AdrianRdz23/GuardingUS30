          using GuardingUS30.Models;
using GuardingUS30.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Controllers
{
    public class ResidentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        //Method to see the complete message that the resident has received
        public IActionResult MessageView(int id, string username)
        {
            //convert the id sender to int
            int id2 = Int32.Parse(username);


            UsersDAO updateDAO = new UsersDAO();

            updateDAO.UpdateStatus(id,id2);
            
            UsersDAO usersDAO = new UsersDAO();

            //Method to look for the message that the resident clicked
            UserNotificationModel mymessage = usersDAO.findMessageById(id, id2);




            return View(mymessage);
        }


        //Action to Delete Notifications
        public IActionResult DeleteNotification(int id, string username)
        {
            int id2 = Int32.Parse(username);

            UsersDAO usersDAO = new UsersDAO();

            usersDAO.DeleteNotification(id, id2);


            UserNotificationModel myuser = new UserNotificationModel();
            myuser.user = new UserModel();
            myuser.user.iduser = id2;
            myuser.notification = new NotificationsModel();

            myuser.iduser = id2;



            UsersDAO usersDAO2 = new UsersDAO();


            List<GuardingUS30.Models.UserNotificationModel> notifications = usersDAO2.ReadNotifications(myuser);



            return View("SeeNotificationView", notifications);
        }

        public IActionResult SeeNotificationView(UserNotificationModel myuser)
        {
            //int iduser = 0;
            //UserNotificationModel myuser = new UserNotificationModel();
            //myuser.user = new UserModel();
            //myuser.user.iduser = iduser;
            //myuser.notification = new NotificationsModel();

            myuser.iduser = myuser.user.iduser;



            UsersDAO usersDAO = new UsersDAO();


            List<GuardingUS30.Models.UserNotificationModel> notifications = usersDAO.ReadNotifications(myuser);




            // display the notification form.  Step 1 in sending a notification.
            return View(notifications);
        }
    }
}
