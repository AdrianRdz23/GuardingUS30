using GuardingUS30.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Services
{
    public class AdminDAO
    {

        //String that contains the connection of your data base
        string connectionString = @"Data Source=DESKTOP-FSADQVG;Initial Catalog = guardingmx; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Method to read all roles
        public List<RoleModel> ReadRoles()
        {
            //bool success = false;

            List<RoleModel> list = new List<RoleModel>();

            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT * FROM dbo.roles";

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

                        RoleModel myrole = new RoleModel();

                        myrole.idrole = (int)reader["idrole"];
                        myrole.name = (string)reader["name"];
                        myrole.status = Convert.ToBoolean(reader["status"]);
                        myrole.startDate = (DateTime)reader["startDate"];
                        myrole.modificationDate = (DateTime)reader["modificationDate"];

                        list.Add(myrole);

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

        //Method to read all homes
        public List<UserHomeModel> ReadHomes()
        {
            //bool success = false;

            List<UserHomeModel> list = new List<UserHomeModel>();

            //sql statement to select  homes and user by using JOIN
            //string sqlStatement = "SELECT * FROM dbo.home WHERE status=0";
            string sqlStatement = "SELECT h.idhome, u.[name] , h.number, h.cars, h.address, h.[status], h.startDate, h.modificationDate from home h join users u on h.iduser = u.iduser WHERE h.[status] = 0";


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


                    //Use while to able to grab the homes information
                    while (reader.Read())
                    {

                        //Create a model for user and home
                        UserHomeModel myhome = new UserHomeModel();
                        myhome.homeModel = new HomeModel();
                        myhome.userModel = new UserModel();

                        myhome.homeModel.idhome = (int)reader["idhome"];
                        myhome.userModel.name = (string)reader["name"];
                        myhome.homeModel.number = (int)reader["number"];
                        myhome.homeModel.cars = (int)reader["cars"];
                        myhome.homeModel.address = (string)reader["address"];
                        myhome.homeModel.status = Convert.ToByte(reader["status"]);
                        myhome.homeModel.startDate = (DateTime)reader["startDate"];
                        myhome.homeModel.modificationDate = (DateTime)reader["modificationDate"];

                        list.Add(myhome);

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

        
        public List<HomeModel> ReadHomesv2()
        {
            //bool success = false;

            List<HomeModel> list = new List<HomeModel>();

            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT * FROM dbo.home WHERE status=0";
            //string sqlStatement = "SELECT h.idhome, u.[name] , h.number, h.cars, h.address, h.[status], h.startDate, h.modificationDate from home h join users u on h.iduser = u.iduser WHERE h.[status] = 0";


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

                        HomeModel myhome = new HomeModel();
                        //myhome.homeModel = new HomeModel();
                        //myhome.userModel = new UserModel();

                        myhome.idhome = (int)reader["idhome"];
                        myhome.number = (int)reader["number"];
                        myhome.cars = (int)reader["cars"];
                        myhome.address = (string)reader["address"];
                        myhome.iduser = (int)reader["iduser"];
                        myhome.status = Convert.ToByte(reader["status"]);
                        myhome.startDate = (DateTime)reader["startDate"];
                        myhome.modificationDate = (DateTime)reader["modificationDate"];

                        list.Add(myhome);

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
        


        //Method to read all users
        public List<UserModel> ReadUsers()
        {
            //bool success = false;

            //List<UserRoleModel> list = new List<UserRoleModel>();
            List<UserModel> list = new List<UserModel>();



            //sql statement to check the users: the name and password
            string sqlStatement = "SELECT * FROM dbo.users WHERE status=0";
            //string sqlStatement = "SELECT u.iduser, u.[name] as username, u.email, u.phonenumber, r.[name], u.[password], u.[status], u.startDate, u.modificationDate from users u join roles r on u.idrole = r.idrole WHERE u.[status] = 0";

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
                        UserModel user = new UserModel();



                        user.iduser = (int)reader["iduser"];
                        user.name = (string)reader["name"];
                        user.idrole = (int)reader["idrole"];
                        user.password = (string)reader["password"];

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



        //Method to read all the users 
        public List<UserRoleModel> ReadUsersv2()
        {

            //List<UserRoleModel> list = new List<UserRoleModel>();
            List<UserRoleModel> list = new List<UserRoleModel>();



            //sql statement to check the users: the name and password
            //string sqlStatement = "SELECT * FROM dbo.users WHERE status=0";
            string sqlStatement = "SELECT u.iduser, u.[name] as username, u.email, u.phonenumber, r.[name], u.[password], u.[status], u.startDate, u.modificationDate from users u join roles r on u.idrole = r.idrole WHERE u.[status] = 0";

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
                        UserRoleModel user = new UserRoleModel();
                        user.userModel = new UserModel();
                        user.roleModel = new RoleModel();


                        user.userModel.iduser = (int)reader["iduser"];
                        user.userModel.name = (string)reader["username"];
                        user.userModel.email = (string)reader["email"];
                        user.userModel.phonenumber = (string)reader["phonenumber"];
                        //user.userModel.idrole = (int)reader["idrole"];
                        user.roleModel.name = (string)reader["name"];
                        user.userModel.password = (string)reader["password"];
                        //userole.userModel.status = Convert.ToBoolean(reader["status"]);
                        user.userModel.startDate = (DateTime)reader["startDate"];
                        user.userModel.modificationDate = (DateTime)reader["modificationDate"];
                        //userole.userModel.idrole = (int)reader["idrole"];

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

        //Method to find home by id
        public HomeModel findHomeById(int id)
        {
            //Create a model for home 
            HomeModel myhome = new HomeModel();


            //sql statement to find home
            string sqlStatement = "SELECT * FROM dbo.home WHERE status=0 AND idhome=@idhome";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Add parameters
                command.Parameters.Add("@idhome", System.Data.SqlDbType.Int).Value = id;



                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();


                    //While method to get home's information
                    while (reader.Read())
                    {

                        //RoleModel myrole = new RoleModel();

                        myhome.idhome = (int)reader["idhome"];
                        myhome.number = (int)reader["number"];
                        myhome.cars = (int)reader["cars"];
                        myhome.address = (string)reader["address"];
                        myhome.iduser = (int)reader["iduser"];                    
                        myhome.status = Convert.ToByte(reader["status"]);
                        myhome.startDate = (DateTime)reader["startDate"];
                        myhome.modificationDate = (DateTime)reader["modificationDate"];

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
            return myhome;
        }

        //Method to find home by id
        public VisitorModel findVisitorById(int id)
        {
            //Create a model for home 
            VisitorModel myhome = new VisitorModel();


            //sql statement to find home
            string sqlStatement = "SELECT * FROM dbo.visitors WHERE status=0 AND idvisitor=@idvisitor";

            //convert sql Statement to a Sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //create a command that comes with the sql statement and the connection
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Add parameters
                command.Parameters.Add("@idvisitor", System.Data.SqlDbType.Int).Value = id;



                try
                {
                    //You open the connection
                    connection.Open();

                    //Use sql data reader to read 
                    SqlDataReader reader = command.ExecuteReader();


                    //While method to get home's information
                    while (reader.Read())
                    {

                        //RoleModel myrole = new RoleModel();

                        myhome.idvisitor = (int)reader["idhome"];
                        myhome.name = (string)reader["name"];
                        myhome.carPlate = (string)reader["carPlate"];
                        myhome.entrance = (DateTime)reader["entrance"];
                        myhome.exit = (DateTime)reader["exit"];
                        myhome.identification = (string)reader["identification"];
                        myhome.idhome = (int)reader["idhome"];
                        myhome.status = Convert.ToBoolean(reader["status"]);
                        myhome.startDate = (DateTime)reader["startDate"];
                        myhome.modificationDate = (DateTime)reader["modificationDate"];

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
            return myhome;
        }

        //Method to create add role on the database
        public bool InsertRole(RoleModel role)
        {
            bool success = false;
            try
            {

                //sql statement to add roles
                string sqlStatement = "INSERT roles ([name],[status], startDate, modificationDate) VALUES (@name,0,GETDATE(),cast(-53690 as datetime));";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add a parameter, which is name
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = role.name;



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

        //Method to update a role on the database
        public bool UpdateRole(RoleModel role)
        {
            bool success = false;
            try
            {

                //sql statement to add users
                string sqlStatement = "UPDATE roles set name = @name, modificationDate = GETDATE() WHERE idrole = @idrole;";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add two parameters: name and the idrole
                    command.Parameters.Add("@idrole", System.Data.SqlDbType.Int).Value = role.idrole;
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = role.name;
                    



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

        //Method to delete a role on the database
        public bool DeleteRole(int id)
        {
            
            bool success = false;
            try
            {

                //sql statement to delete roles
                //string sqlStatement = "DELETE FROM roles WHERE idrole=@idrole";
                string sqlStatement = "UPDATE roles set status=1, modificationDate=GETDATE() WHERE idrole=@idrole";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameter
                    command.Parameters.Add("@idrole", System.Data.SqlDbType.Int).Value = id;




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


        //Method to create add user on the database
        public bool InsertUser(UserModel user)
        {
            bool success = false;
            try
            {

                //sql statement to add users
                string sqlStatement = "INSERT users ([name], email, phonenumber, idrole, [password], [status], startDate, modificationDate) VALUES (@name,@email,@phonenumber,@idrole,@password,0,GETDATE(),cast(-53690 as datetime));";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add five parameters, whihc are the username, email, phonenumber, idrole,password
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.name;
                    command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 100).Value = user.email;
                    command.Parameters.Add("@phonenumber", System.Data.SqlDbType.VarChar, 50).Value = user.phonenumber;
                    command.Parameters.Add("@idrole", System.Data.SqlDbType.Int).Value = user.idrole;
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        //Method to update a role on the database
        /*
        public bool UpdateUser(UserModel user)
        {
            bool success = false;
            try
            {

                //sql statement to add users
                string sqlStatement = "UPDATE users set [name] = @name, email=@email, phonenumber = @phonenumber, idrole = @idrole, [password] = @password, modificationDate = GETDATE() WHERE iduser = @iduser;";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add two parameters: name and the idrole
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = user.iduser;
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.name;
                    command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 100).Value = user.email;
                    command.Parameters.Add("@phonenumber", System.Data.SqlDbType.VarChar, 50).Value = user.phonenumber;
                    command.Parameters.Add("@idrole", System.Data.SqlDbType.Int).Value = user.idrole;
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }
        */

        //Method to update a user
        public bool UpdateUser(UserRoleModel user)
        {
            bool success = false;
            try
            {

                //sql statement to update user
                string sqlStatement = "UPDATE users set [name] = @name, email=@email, phonenumber = @phonenumber, idrole = @idrole, [password] = @password, modificationDate = GETDATE() WHERE iduser = @iduser;";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameters
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = user.userModel.iduser;
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = user.userModel.name;
                    command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 100).Value = user.userModel.email;
                    command.Parameters.Add("@phonenumber", System.Data.SqlDbType.VarChar, 50).Value = user.userModel.phonenumber;
                    command.Parameters.Add("@idrole", System.Data.SqlDbType.Int).Value = user.userModel.idrole;
                    command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.userModel.password;




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

        //Method to update home
        public bool UpdateHome(UserHomeModel home)
        {
            bool success = false;
            try
            {

                //sql statement to update home
                string sqlStatement = "UPDATE home set iduser=@iduser, number = @number, cars = @cars, address = @address, modificationDate = GETDATE() WHERE idhome = @idhome;";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameters
                    command.Parameters.Add("@idhome", System.Data.SqlDbType.Int).Value = home.homeModel.idhome;
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = home.homeModel.iduser;
                    command.Parameters.Add("@number", System.Data.SqlDbType.Int).Value = home.homeModel.number;
                    command.Parameters.Add("@cars", System.Data.SqlDbType.Int).Value = home.homeModel.cars;
                    command.Parameters.Add("@address", System.Data.SqlDbType.VarChar, 100).Value = home.homeModel.address;




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


        //Method to delete a user on the database
        public bool DeleteUser(int id)
        {

            bool success = false;
            try
            {

                //sql statement to add users
                //string sqlStatement = "DELETE FROM users WHERE iduser=@iduser";
                string sqlStatement = "UPDATE users set status=1, modificationDate=GETDATE() WHERE iduser=@iduser";



                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add two parameters: name and the idrole
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = id;




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


        //Method to add home 
        public bool InsertHome(UserHomeModel user)
        {
            bool success = false;
            try
            {

                //sql statement to add home
                string sqlStatement = "INSERT home (iduser, number, cars, address, [status], startDate, modificationDate) VALUES (@iduser,@number,@cars,@address,0,GETDATE(),cast(-53690 as datetime));";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add four parameters
                    command.Parameters.Add("@iduser", System.Data.SqlDbType.Int).Value = user.homeModel.iduser;
                    command.Parameters.Add("@number", System.Data.SqlDbType.Int).Value = user.homeModel.number;
                    command.Parameters.Add("@cars", System.Data.SqlDbType.Int).Value = user.homeModel.cars;
                    command.Parameters.Add("@address", System.Data.SqlDbType.VarChar, 200).Value = user.homeModel.address;


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

        //Method to delete home
        public bool DeleteHome(int id)
        {

            bool success = false;
            try
            {

                //sql statement to add users
                //string sqlStatement = "DELETE FROM roles WHERE idrole=@idrole";
                string sqlStatement = "UPDATE home set status=1, modificationDate=GETDATE() WHERE idhome=@idhome";

                //convert sql Statement to a Sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //create a command that comes with the sql statement and the connection
                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    //Add parameter
                    command.Parameters.Add("@idhome", System.Data.SqlDbType.Int).Value = id;


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

        //Method to read visitor on your database
        public List<VisitorsHomeModel> ReadVisitors()
        {
 
            //Make a list to get the visitor's information
            List<VisitorsHomeModel> list = new List<VisitorsHomeModel>();

            //sql statement to check the users: the name and password
            //string sqlStatement = "SELECT * FROM dbo.home WHERE status=0";
            string sqlStatement = "SELECT v.idvisitor, v.[name], v.carPlate, v.entrance, v.[exit], v.[identification], h.number, h.[address] from visitors v join home h on v.idhome = h.idhome";


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


                    //While method to be able to get all the visitor's information
                    while (reader.Read())
                    {
                        //add a visitor model with a home model
                        VisitorsHomeModel visitor = new VisitorsHomeModel();

                        visitor.visitors = new VisitorModel();
                        visitor.home = new HomeModel();

                        visitor.visitors.idvisitor = (int)reader["idvisitor"];
                        visitor.visitors.name = (string)reader["name"];
                        visitor.visitors.carPlate = (string)reader["carPlate"];
                        visitor.visitors.entrance = (DateTime)reader["entrance"];
                        visitor.visitors.exit = (DateTime)reader["cars"];
                        visitor.home.address = (string)reader["address"];
                        visitor.visitors.status = Convert.ToBoolean(reader["status"]);
                        visitor.visitors.startDate = (DateTime)reader["startDate"];
                        visitor.visitors.modificationDate = (DateTime)reader["modificationDate"];

                        list.Add(visitor);

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



    }
}
