using GuardingUS30.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Services
{
    public class SecurityGuardDAO
    {
        //String that contains the connection of your data base
        string connectionString = @"Data Source=DESKTOP-FSADQVG;Initial Catalog = guardingmx; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //Method to delete home
        public bool UpdateExit(int id)
        {

            bool success = false;
            try
            {

                //sql statement to add users
                //string sqlStatement = "DELETE FROM roles WHERE idrole=@idrole";
                string sqlStatement = "UPDATE visitors set [exit]=GETDATE() WHERE idvisitor=@idvisitor";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameter
                    command.Parameters.Add("@idvisitor", System.Data.SqlDbType.Int).Value = id;


                    try
                    {
                        //Open the connection
                        connection.Open();

                        //Apply the query that you typed, which is your sql statement
                        command.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        //Close the connection
                        connection.Close();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        //Method to add visitor in your datasbase
        public bool InsertVisitor(VisitorsHomeModel visitor)
        {
            bool success = false;
            try
            {

                //sql statement to add users
                string sqlStatement = "INSERT visitors ([name], carPlate, entrance, [exit], identification, idhome, [description],[status],startDate, modificationDate) VALUES (@name,@carPlate,GETDATE(),cast(-53690 as datetime),@identification,@idhome,@description,0,GETDATE(),cast(-53690 as datetime))";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameters
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = visitor.visitors.name;
                    command.Parameters.Add("@carPlate", System.Data.SqlDbType.VarChar).Value = visitor.visitors.carPlate;
                    //command.Parameters.Add("@entrance", System.Data.SqlDbType.DateTime).Value = visitor.visitors.entrance;
                    //command.Parameters.Add("@exit", System.Data.SqlDbType.DateTime).Value = visitor.visitors.exit;
                    command.Parameters.Add("@identification", System.Data.SqlDbType.VarChar, 200).Value = visitor.visitors.identification;
                    command.Parameters.Add("@idhome", System.Data.SqlDbType.Int).Value = visitor.visitors.idhome;
                    command.Parameters.Add("@description", System.Data.SqlDbType.VarChar, 200).Value = visitor.visitors.description;


                    try
                    {
                        //Open the connection
                        connection.Open();

                        //Apply the query that you typed, which is your sql statement
                        command.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        //Close the connection
                        connection.Close();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        //Method to read all roles
        public List<VisitorsHomeModel> ReadVisitors()
        {
            //bool success = false;

            List<VisitorsHomeModel> list = new List<VisitorsHomeModel>();

            //sql statement to check the users: the name and password
                string sqlStatement = "SELECT v.idvisitor, v.[name], v.carPlate, v.entrance, v.[exit], v.[identification], h.number, h.[address], v.[description] from visitors v join home h on v.idhome = h.idhome";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {

                        VisitorsHomeModel myvisitor = new VisitorsHomeModel();
                        myvisitor.visitors = new VisitorModel();
                        myvisitor.home = new HomeModel();

                        myvisitor.visitors.idvisitor = (int)reader["idvisitor"];
                        myvisitor.visitors.name = (string)reader["name"];
                        myvisitor.visitors.carPlate = (string)reader["carPlate"];
                        myvisitor.home.address = (string)reader["address"];
                        myvisitor.visitors.identification = (string)reader["identification"];
                        myvisitor.visitors.entrance = (DateTime)reader["entrance"];
                        myvisitor.visitors.exit = (DateTime)reader["exit"];

                        list.Add(myvisitor);

                    }




                }
                catch (Exception e)
                {
                    //Error Message
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //Close the connection
                    connection.Close();
                }


            }
            return list;
        }

        //Method to add notification in your datasbase
        public int InsertNotification(UserNotificationModel notification)
        {
            int notificationid = 0;
            try
            {

                //sql statement to add users
                string sqlStatement = "INSERT notifications (title, iduser, [message],[status], startDate, modificationDate) VALUES (@title,@iduser,@message,0,GETDATE(),cast(-53690 as datetime)); SELECT SCOPE_IDENTITY();";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameters
                    command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 100).Value = notification.notification.title;
                    command.Parameters.Add("@message", System.Data.SqlDbType.VarChar).Value = notification.notification.message;
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = notification.notification.iduser;


                    try
                    {
                        //Open the connection
                        connection.Open();

                        //Apply the query that you typed, which is your sql statement
                        //command.ExecuteNonQuery();
                        object myobject = command.ExecuteScalar();
                        if(myobject != null)
                        {
                            int.TryParse(myobject.ToString(), out notificationid);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        //Close the connection
                        connection.Close();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return notificationid;
        }

        //Method to read all users
        public List<UserNotificationModel> ReadUsers(int notificationid)
        {

            List<UserNotificationModel> list = new List<UserNotificationModel>();



            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT * FROM dbo.users WHERE status=0 AND idrole=1";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();


                    //Use while to get all the users to the list
                    while (reader.Read())
                    {
                        UserNotificationModel user = new UserNotificationModel();
                        user.user = new UserModel();
                        user.notification = new NotificationsModel();

                        user.notification.idnotification = notificationid;
                        user.user.iduser = (int)reader["iduser"];
                        user.user.name = (string)reader["name"];
                        user.user.email = (string)reader["email"];
                        user.user.phonenumber = (string)reader["phonenumber"];


                        //Add all the users to the list
                        list.Add(user);

                    }




                }
                catch (Exception e)
                {
                    //Error Message
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //Close the connection
                    connection.Close();
                }


            }
            return list;
        }


        //Method to add notification in your datasbase
        public bool SendUser(UserNotificationModel notification)
        {
            bool success = false;
            try
            {

                //sql statement to add users
                string sqlStatement = "INSERT usernotifications (iduser,idnotification,[status], startDate,modificationDate) VALUES (@iduser,@idnotification,0,GETDATE(),cast(-53690 as datetime));";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameters
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = notification.user.iduser;
                    command.Parameters.Add("@idnotification", System.Data.SqlDbType.Int).Value = notification.notification.idnotification;



                    try
                    {
                        //Open the connection
                        connection.Open();

                        //Apply the query that you typed, which is your sql statement
                        command.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        //Close the connection
                        connection.Close();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        //Method to find user by id
        public UserModel findUserById(int id)
        {

            //List<UserModel> list = new List<UserModel>();
            UserModel myuser = new UserModel();


            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT * FROM dbo.users WHERE status=0 AND iduser=@iduser";
            //string sqlStatement = "SELECT u.iduser, u.[name], u.email, u.phonenumber, r.[name], u.[password], u.[status], u.startDate, u.modificationDate from users u join roles r on u.idrole = r.idrole WHERE u.[status] = 0";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = id;



                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {

                        //RoleModel myrole = new RoleModel();

                        myuser.iduser = (int)reader["iduser"];
                        myuser.name = (string)reader["name"];
                        myuser.email = (string)reader["email"];
                        myuser.phonenumber = (string)reader["phonenumber"];
                        myuser.idrole = (int)reader["idrole"];
                        myuser.password = (string)reader["password"];
                        myuser.status = Convert.ToBoolean(reader["status"]);
                        myuser.startDate = (DateTime)reader["startDate"];
                        myuser.modificationDate = (DateTime)reader["modificationDate"];

                        //list.Add(myuser);

                    }




                }
                catch (Exception e)
                {
                    //Error Message
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //Close the connection
                    connection.Close();
                }


            }
            return myuser;
        }
    }
}
