using GuardingUS30.Models;
using GuardingUS30.Services;
using GuardingUS30.Views.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace GuardingUS30.Controllers
{
    public class LoginController : Controller
    {

        private readonly DBCtx _context;

        public LoginController(DBCtx context)
        {
            _context = context;
        }
        //Public method to go to the Login Form
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SecurityGuardView()
        {
            return View();
        }

        public IActionResult AdminView()
        {
            return View();
        }

        //A method to check if the User logged in correctly
        public IActionResult ProcessLogin(UserModel userModel)
        {

            UsersDAO usersDAO = new UsersDAO();

            //if statement to know if the User exists or not. If the user is inside of the database or not.
            //UserModel user = usersDAO.FindUserByNameandPassword(userModel);

            UserModel user = usersDAO.FindUserByNameandPassword(userModel);


            switch (user.idrole)
            {
                case 1:
                    return View("ResidentView", user);
                case 2:
                    return View("AdminView", user);
                
                case 3:
                    return View("SecurityGuardView", user);
                    
                default:
                    return View("LoginFailure", user);
                   
            }
           
        }

        //An action to go to Admin Catalog View
        public IActionResult AdminCatalog()
        {
            return View();
        }

        //Action to logout
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        //An action to go to the Role View
        public IActionResult RoleList()
        {
            AdminDAO adminDAO = new AdminDAO();
            //Make listo to get the roles
            List<GuardingUS30.Models.RoleModel> everyone = adminDAO.ReadRoles();

            return View("RoleView", everyone);
        }

        //Action to go to Home View
        public IActionResult HomeList()
        {
            AdminDAO adminDAO = new AdminDAO();

            //Make a listo get homes
            List<GuardingUS30.Models.UserHomeModel> everyone = adminDAO.ReadHomes();

            return View("HomeView", everyone);
        }

        //Action to go to User View
        public IActionResult UserList()
        {
            // supposed to display a list of all users in the users table.

            AdminDAO adminDAO = new AdminDAO();
            //List to get the users
            List<GuardingUS30.Models.UserRoleModel> everyone = adminDAO.ReadUsersv2();

            return View("UserView" , everyone);
        }

        //Action to go to add home view
        public IActionResult AddHomeView()
        {

            AdminDAO adminDAO = new AdminDAO();
            HomeModel homeModel = new HomeModel();

            //List to get users
            List<UserModel> user = adminDAO.ReadUsers();

            //List to get the home names
            List<String[]> homeModelNames = new List<String[]>();

            foreach (var item in user)
            {
                String[] line = { item.iduser.ToString(), item.name };
                homeModelNames.Add(line);
            }

            UserHomeModel uhm = new UserHomeModel(homeModel, homeModelNames);

            return View(uhm);
        }

        //Action to go to Admin View 
        public IActionResult AddHomeProcess(UserHomeModel myhomeModel)
        {

            AdminDAO adminDAO = new AdminDAO();

            //Apply the method to insert a user on the database
            adminDAO.InsertHome(myhomeModel);



            return View("AdminView");
        }

        //Action to Delete home
        public IActionResult DeleteHome(int id)
        {
            AdminDAO adminDAO = new AdminDAO();

            adminDAO.DeleteHome(id);

            //list with user and home information
            List<GuardingUS30.Models.UserHomeModel> everyone = adminDAO.ReadHomes();
            //  List<RoleModel> roles = adminDAO.ReadRoles();

            return View("HomeView", everyone);
        }

        //Action to go to the Add Role View
        public IActionResult AddRoleView()
        {
            return View(); 
        }

        //Action to go Add Role, and go to Admin View
        public IActionResult AddRoleProcess(RoleModel roleModel)
        {

            AdminDAO adminDAO = new AdminDAO();

            //Apply the method to insert a role on the database
            adminDAO.InsertRole(roleModel);

            

            return View("AdminView");
        }


        //Action to go to Edit Role View 
        public IActionResult EditRoleView(int id )
        {
            RoleModel roleModel = new RoleModel();
            AdminDAO adminDAO = new AdminDAO();

            return View(adminDAO.ReadRoles().Find(roleModel => roleModel.idrole == id));
        }

        //Action to edit role and go to Admin View
        public IActionResult EditRoleProcess(RoleModel roleModel)
        {
            AdminDAO adminDao = new AdminDAO();
            adminDao.UpdateRole(roleModel);

            return View("AdminView");
        }

        //Action to delete role
        public IActionResult DeleteRole(int id)
        {
            AdminDAO adminDAO = new AdminDAO();

            adminDAO.DeleteRole(id);

            List<GuardingUS30.Models.RoleModel> everyone = adminDAO.ReadRoles();

            return View("RoleView", everyone);
        }

        //Action to go Add User View
        public IActionResult AddUserView()
        {
            return View();
        }

        //Action to add user and go to Admin View
        public IActionResult AddUserProcess(UserModel userModel)
        {

            AdminDAO adminDAO = new AdminDAO();

            //Apply the method to insert a user on the database
            adminDAO.InsertUser(userModel);



            return View("AdminView");
        }


        //Action go to Edit Home View
        public IActionResult EditHomeView(int id)
        {
            AdminDAO adminDAO = new AdminDAO();
            HomeModel homeModel = adminDAO.findHomeById(id);

            List<UserModel> user = adminDAO.ReadUsers();
            //List<HomeModel> home = adminDAO.ReadHomes();
            List<String[]> homeModelNames = new List<String[]>();

            foreach (var item in user)
            {
                String[] line = { item.iduser.ToString(), item.name };
                homeModelNames.Add(line);
            }

            UserHomeModel uhm = new UserHomeModel(homeModel, homeModelNames);

            return View(uhm);
        }

        //Action to go to Edit User View
        public IActionResult EditUserView(int id)
        {
            AdminDAO adminDAO = new AdminDAO();
            UserModel userModel = adminDAO.findUserById(id);
            //\.Find(userModel => userModel.userModel.iduser == id);


            //UserRoleModel rolelist = new UserRoleModel();
            // userModel.rolelist = new SelectList(adminDAO.ReadUsers(), "idrole","name");
            List<RoleModel> roles = adminDAO.ReadRoles();
            List<String[]> roleModelNames = new List<String[]>();

            foreach (var item in roles)
            {
                //roleModelNames.Add(item.name);

                String[] line = { item.idrole.ToString(), item.name };
                roleModelNames.Add(line);
            }
            

            //roleModelNames.Select(n => new SelectListItem { Text = n, Value = n });


            UserRoleModel urm = new UserRoleModel(userModel, roleModelNames);

            return View(urm);
        }

        //Actionn to user's information and go to Admin View
        public IActionResult EditUserProcess(UserRoleModel userModelv2)
        {
            AdminDAO adminDao = new AdminDAO();
            adminDao.UpdateUser(userModelv2);

            return View("AdminView");
        }

        //Action to edit home information and go to Admin View
        public IActionResult EditHomeProcess(UserHomeModel homeModelv2)
        {
            AdminDAO adminDao = new AdminDAO();
            adminDao.UpdateHome(homeModelv2);

            return View("AdminView");
        }

        //Action to Delete A user
        public IActionResult DeleteUser(int id)
        {
            AdminDAO adminDAO = new AdminDAO();

            adminDAO.DeleteUser(id);

            List<GuardingUS30.Models.UserRoleModel> everyone = adminDAO.ReadUsersv2();
          //  List<RoleModel> roles = adminDAO.ReadRoles();

            return View("UserView", everyone);
        }

        //Action to go to Entrance/Exit View 
        public IActionResult EntrancesExitsView()
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();
            //Make listo to get the roles
            List<GuardingUS30.Models.VisitorsHomeModel> everyone = guardDAO.ReadVisitors();

            return View("EntrancesExitsView", everyone);
        }

        //Action to go to Entrance/Exit View 
        public IActionResult EntrancesExits2View()
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();
            //Make listo to get the roles
            List<GuardingUS30.Models.VisitorsHomeModel> everyone = guardDAO.ReadVisitors();

            return View("EntrancesExits2View", everyone);
        }

        //Action to go to add entrance View
        public IActionResult AddEntranceView2()
        {

            AdminDAO adminDAO = new AdminDAO();
            VisitorModel visitorModel = new VisitorModel();

            //List to get users
            List<UserHomeModel> myhome = adminDAO.ReadHomes();

            //List to get the home names
            List<String[]> homeModelInfo = new List<String[]>();

            foreach (var item in myhome)
            {
                String[] line = { item.homeModel.idhome.ToString(), item.homeModel.address, item.userModel.name, item.homeModel.number.ToString() };
                homeModelInfo.Add(line);
            }

            VisitorsHomeModel vhm = new VisitorsHomeModel(visitorModel, homeModelInfo);

            return View(vhm);
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
            


            return Json(new SelectList(numberlist,"idhome", "number"));
        }

        //Mehtod to select the number of home once the address has been selected first
        public JsonResult GetHomeNumber(string idhome)
        {


            var homenumber = _context.Home.Where(x => x.idhome == int.Parse(idhome)).Select(x=> new { x.idhome }).ToList();

            

            return Json(new SelectList(homenumber, "idhome"));
        }



        //Action to add a visitor his/her entrance 
        public IActionResult AddEntranceProcess(VisitorsHomeModel myvisitorModel)
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            //Apply the method to insert a visitor's entrance on the database
            guardDAO.InsertVisitor(myvisitorModel);



            return View("SecurityGuardView");
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

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            foreach (var user in userNotificationListViewModel.NotificationsList)
            {
                //Apply the method to send a user the notification on the database
                guardDAO.SendUser(user);
            }

            return View("SecurityGuardView");
        }

        public IActionResult Notification2View()
        {
            return View();
        }

        //Action to add a notification 
        public IActionResult NotificationProcess2(UserNotificationModel userNotificationModel)
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            //Apply the method to insert a visitor's entrance on the database
            int notid = guardDAO.InsertNotification(userNotificationModel);

            SecurityGuardDAO guardDAO2 = new SecurityGuardDAO();

            //List to get the users
            List<GuardingUS30.Models.UserNotificationModel> everyone = guardDAO2.ReadUsers(notid);

            return View("SendNotification2View", everyone);
        }



        public IActionResult SendNotificationProcess2(List<UserNotificationModel> userNotificationModel)
        {

            SecurityGuardDAO guardDAO = new SecurityGuardDAO();

            foreach (var user in userNotificationModel)
            {
                //Apply the method to send a user the notification on the database
                guardDAO.SendUser(user);
            }

            return View("SecurityGuardView");
        }
    }
}
