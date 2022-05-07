using GuardingUS30.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Services
{
    public class UsersDAO
    {
        //String that contains the connection of your data base
        string connectionString = @"Data Source=DESKTOP-FSADQVG;Initial Catalog = guardingmx; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Method to find your user's name and password
        public UserModel FindUserByNameandPassword(UserModel user)
        {
            //bool success = false;



            UserModel myuser = new UserModel();

            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT * FROM dbo.users WHERE name = @name AND password = @password";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Add two parameters, whihc are the username and password
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.name;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.password;

                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read the username and password
                    SqlDataReader reader = command.ExecuteReader();

                    

                    while (reader.Read())
                    {
                        //success = true;

                        myuser.iduser = (int)reader["iduser"];
                        myuser.name = (string)reader["name"];
                        myuser.email = (string)reader["email"];
                        myuser.phonenumber = (string)reader["phonenumber"];
                        myuser.idrole = (int)reader["idrole"];
                        myuser.password = (string)reader["password"];
                        myuser.status = (bool)reader["status"];
                        myuser.startDate = (DateTime)reader["startDate"];
                        myuser.modificationDate = (DateTime)reader["modificationDate"];
                    }
                    
                    
                       
                    
                }catch(Exception e)
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

        //Method to find your user's name and password
        public UserNotificationModel FindUserByNameandPasswordBeta(UserNotificationModel user)
        {
            //bool success = false;

              UserNotificationModel myuser = new UserNotificationModel();
            myuser.notification = new NotificationsModel();
            myuser.user = new UserModel();

            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT COUNT(n.idnotification) as 'mymessages', u.iduser, u.[password], u.idrole , u.[name] from usernotifications n join users u on n.iduser = u.iduser WHERE u.[name]=@name AND u.[password]=@password AND n.[status]= 0 AND u.[status]=0 GROUP BY u.[name] , u.iduser, u.[password], u.idrole";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Add two parameters, whihc are the username and password
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.user.name;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.user.password;

                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read the username and password
                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {

                        myuser.user.iduser = (int)reader["iduser"];
                        myuser.user.name = (string)reader["name"];
                        myuser.user.idrole = (int)reader["idrole"];
                        myuser.user.password = (string)reader["password"];
                        myuser.mymessages = (int)reader["mymessages"];
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


        //Method to find your user's name and password
        public UserNotificationModel FindUserByNameandPasswordNoMessages(UserNotificationModel user)
        {
            //bool success = false;

            UserNotificationModel myuser = new UserNotificationModel();
            myuser.notification = new NotificationsModel();
            myuser.user = new UserModel();

            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT u.iduser, u.[password], u.idrole , u.[name] from usernotifications n join users u on n.iduser = u.iduser WHERE u.[name]=@name AND u.[password]=@password AND u.[status]=0 GROUP BY u.[name] , u.iduser, u.[password], u.idrole";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Add two parameters, whihc are the username and password
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.user.name;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.user.password;

                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read the username and password
                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {

                        myuser.user.iduser = (int)reader["iduser"];
                        myuser.user.name = (string)reader["name"];
                        myuser.user.idrole = (int)reader["idrole"];
                        myuser.user.password = (string)reader["password"];
                        myuser.mymessages = (int)reader["mymessages"];
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

        //Method to create add user on the database
        public bool InsertUser(UserModel user)
        {
            bool success = false;
            try
            {

                //sql statement to add users
                string sqlStatement = "INSERT users ([name], email, phonenumber, idrole, [password], [status], startDate, modificationDate) VALUES (@name,@email,@phonenumber,1,@password,0,GETDATE(),cast(-53690 as datetime));";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add four parameters, whihc are the username, email, phonenumber, password
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.name;
                    command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 100).Value = user.email;
                    command.Parameters.Add("@phonenumber", System.Data.SqlDbType.VarChar, 50).Value = user.phonenumber;
                    command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.password;


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
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        public List<UserNotificationModel> ReadNotifications(UserNotificationModel user)
        {
            //bool success = false;

            List<UserNotificationModel> list = new List<UserNotificationModel>();

            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT un.iduser, u.iduser as senduser, n.idnotification, n.title, u.[name], un.[status] as mystatus from usernotifications un join notifications n on  un.idnotification = n.idnotification join users u on n.iduser = u.iduser WHERE un.iduser=@iduser AND un.[status] in (0,1)";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = user.user.iduser;


                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {

                        UserNotificationModel myuser = new UserNotificationModel();
                        myuser.user = new UserModel();
                        myuser.notification = new NotificationsModel();

                        myuser.iduser = (int)reader["iduser"];
                        myuser.user.iduser = (int)reader["senduser"];
                        myuser.notification.idnotification = (int)reader["idnotification"];
                        myuser.user.name = (string)reader["name"];
                        myuser.notification.title = (string)reader["title"];
                        myuser.status = (Byte)reader["mystatus"];
                        

                        list.Add(myuser);

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

        //Method to find home by id
        public UserNotificationModel findMessageById(int id,int id2)
        {
            //Create a model for home 
            UserNotificationModel notification = new UserNotificationModel();
            notification.user = new UserModel();
            notification.notification = new NotificationsModel();


            //sql statement to find home
            string sqlStatement = "SELECT n.title, u.[name], n.[message], n.startDate as notificationDate from usernotifications un join notifications n on  un.idnotification = n.idnotification join users u on n.iduser = u.iduser WHERE un.iduser=@iduser AND n.[status] in (0,1) AND n.idnotification=@idnotification";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Add parameters
                command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = id2;
                command.Parameters.Add("@idnotification", System.Data.SqlDbType.Int).Value = id;
                



                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();


                    //While method to get home's information
                    while (reader.Read())
                    {


                        notification.notification.title = (string)reader["title"];
                        notification.notification.message = (string)reader["message"];
                        notification.user.name = (string)reader["name"];
                        notification.notification.startDate = (DateTime)reader["notificationDate"];

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
            return notification;
        }

        public bool DeleteNotification(int id,int id2)
        {

            bool success = false;
            try
            {

                //sql statement to add users
                //string sqlStatement = "DELETE FROM users WHERE iduser=@iduser";
                string sqlStatement = "UPDATE usernotifications set [status]=2, modificationDate=GETDATE() WHERE idnotification=@idnotification AND iduser=@iduser";



                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add two parameters: name and the idrole
                    command.Parameters.Add("@idnotification", System.Data.SqlDbType.Int).Value = id;
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = id2;





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

        public bool UpdateStatus(int id, int id2)
        {

            bool success = false;
            try
            {

                //sql statement to add users
                //string sqlStatement = "DELETE FROM roles WHERE idrole=@idrole";
                string sqlStatement = "UPDATE usernotifications set [status]=1 WHERE iduser=@iduser AND idnotification=@idnotification";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameter
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = id2;
                    command.Parameters.Add("@idnotification", System.Data.SqlDbType.Int).Value = id;


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



    }


}
