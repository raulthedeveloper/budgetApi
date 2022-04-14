using System.Collections.Generic;
using System.Data.SqlClient;
using MySqlConnector;
using server.Models;

namespace server.DataAccessLayer
{
    public class UserDal
    {
        // private static string connectionString = "data source=X13\\SQLEXPRESS; database=budget; integrated security=SSPI";
        private static string connectionString = "server=localhost;user=raul;password=Freemind2!;port=3306;database=budget";
        

        private static MySqlConnection conn = new MySqlConnection(connectionString);
        private static string tableName = "budget_users";

        // Will be used as Data Access Layer for Users

        public void CreateUser(Users data)
        {
            try
            {
               MySqlCommand cmd = new MySqlCommand($"INSERT INTO {tableName}(email,password) VALUES ('{data.Email}','{data.Password}')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record Inserted correctly");
            }
            catch (Exception e)
            {

              Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteUser(int id, Users user)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }

        public void EditUser(int id, Users user)
        {
            try
            {
                MySqlCommand cm = new MySqlCommand($"",conn);
                conn.Open();
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                conn.Close();
            }

        }

        public List<Users> GetUsers()
        {
            try
            {
                MySqlCommand sql = new MySqlCommand($"SELECT * FROM {tableName}", conn);
                conn.Open();

               MySqlDataReader reader = sql.ExecuteReader();

                List<Users> users = new List<Users>();

                while (reader.Read())
                {
                    Users user = new Users();
                    user.Id = (int?)reader["id"];
                    user.Email = (string)reader["email"];
                    user.Password = (string)reader["password"]; 

                    users.Add(user);
                }

                return users;
                


            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                conn.Close();
            } 
        }

        public Users GetUser(string email, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT email, password FROM {tableName} WHERE email='{email}' AND password='{password}'", conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                Users users = new Users();

                while (reader.Read())
                {
                    users.Email = (string)reader["email"];  
                    users.Password = (string)reader["password"];
                }

                return users;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

        }
        public int GetUserId(string email, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT id FROM {tableName} WHERE email='{email}' AND password='{password}'", conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                int userId = 0;
                while (reader.Read())
                {
                    userId = (int) reader["id"];
                   
                }

                return userId;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public Users GetUser(int Id)
        {
            try
            {   
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {tableName} WHERE id = '{Id}'",conn);
                conn.Open();

                MySqlDataReader r = cmd.ExecuteReader();

                Users users = new Users();


                while (r.Read())
                {
                    users.Id = (int?)r["id"];
                    users.Email = (string)r["email"];
                }

                return users;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
