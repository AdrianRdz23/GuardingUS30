using GuardingUS30.Models;
using GuardingUS30.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Controllers
{
    public class SecurityGuardController : Controller
    {

        private readonly DBCtx _context;

        public SecurityGuardController(DBCtx context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Action to go to Entrance/Exit View 
        public IActionResult EntrancesExitsView()
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();
            //Make listo to get the roles
            List<GuardingUS30.Models.VisitorsHomeModel> everyone = guardDAO.ReadVisitors();

            return View("EntrancesExitsView", everyone);
        }

        //Action to go to add entrance View
        public IActionResult AddEntranceView()
        {

            List<String> addresslist = new List<String>();

            //Get all the addresses of the home table (non-repeatable names of the address)
            foreach (var address in _context.Home.GroupBy(p => p.address).Select(p => new { address = p.Key }).ToArray())
            {
                addresslist.Add(address.address);
            }



            ViewBag.ListofHome = addresslist;

            return View();
        }




        //Mehtod to select the number of home once the address has been selected first
        public JsonResult GetVisitorHomelist(string address)
        {

            var numberlist = _context.Home.Where(x => x.address == address).Select(x => new { x.idhome, x.number }).ToList();



            return Json(new SelectList(numberlist, "idhome", "number"));
        }

        //Mehtod to select the number of home once the address has been selected first
        public JsonResult GetHomeNumber(string idhome)
        {


            var homenumber = _context.Home.Where(x => x.idhome == int.Parse(idhome)).Select(x => new { x.idhome }).ToList();



            return Json(new SelectList(homenumber, "idhome"));
        }



        //Action to add a visitor his/her entrance 
        public IActionResult AddEntranceProcess(VisitorsHomeModel myvisitorModel)
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            //Apply the method to insert a visitor's entrance on the database
            guardDAO.InsertVisitor(myvisitorModel);



            return View("~/Views/Login/SecurityGuardView.cshtml");
        }

        //Action to Update Visitor
        public IActionResult UpdateVisitor(int id)
        {
            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            guardDAO.UpdateExit(id);

            //list with user and home information
            List<GuardingUS30.Models.VisitorsHomeModel> everyone = guardDAO.ReadVisitors();
            //  List<RoleModel> roles = adminDAO.ReadRoles();

            return View("EntrancesExitsView", everyone);
        }

        public IActionResult NotificationView(int iduser)
        {
            //Get just the id of the one who is about to create a message and send it to the residents
            UserNotificationModel myuser = new UserNotificationModel();
            myuser.user = new UserModel();
            myuser.notification = new NotificationsModel();
            myuser.notification.iduser = iduser;




            // display the notification form.  Step 1 in sending a notification.
            return View(myuser);
        }

        //Action to add a notification 
        public IActionResult NotificationProcess(UserNotificationModel userNotificationModel)
        {
            // step 2 in sending notification.
            // show a list of users. Check the users you want to send the message to.

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            //Apply the method to insert a visitor's entrance on the database
            int notid = guardDAO.InsertNotification(userNotificationModel);


            SecurityGuardDAO guardDAO2 = new SecurityGuardDAO();

            //List to get the users
            // List<GuardingUS30.Models.UserNotificationModel> everyone = guardDAO2.ReadUsers(notid);

            var everyone = guardDAO2.ReadUsers(notid);


            UserNotificationListViewModel notificationList = new UserNotificationListViewModel();
            notificationList.NotificationsList = new List<UserNotificationModel>();

            foreach (var item in everyone)
            {
                notificationList.NotificationsList.Add(item);
            }
            return View("SendNotificationView", notificationList);
        }



        public IActionResult SendNotificationProcess(UserNotificationListViewModel userNotificationListViewModel)
        {

            // step 3 - not visible to the user.  Background process.
            // process each checked user.  Add a notification to the db.
            if (Request.Form["action"] == "Send")
            {
                SecurityGuardDAO guardDAO = new SecurityGuardDAO();

                foreach (var user in userNotificationListViewModel.NotificationsList)
                {
                    if (user.notification.IsSelected == true)
                    {
                        //Apply the method to send a user the notification on the database
                        guardDAO.SendUser(user);
                    }
                }
            }
            else if (Request.Form["action"] == "Send Everyone")
            {
                SecurityGuardDAO guardDAO = new SecurityGuardDAO();

                foreach (var user in userNotificationListViewModel.NotificationsList)
                {
                    //Apply the method to send a user the notification on the database
                    guardDAO.SendUser(user);
                }
            }
            return View("~/Views/Login/SecurityGuardView.cshtml");
        }

    }
}
