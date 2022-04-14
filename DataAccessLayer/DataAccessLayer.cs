using server.Models;
using System.Data.SqlClient;
using MySqlConnector;

namespace server.DataAccessLayer
{
    public class DAL
    {
        private static readonly string tableName = "budget_items";

        // private static string connectionString = "data source=X13\\SQLEXPRESS; database=budget; integrated security=SSPI";
        private static string connectionString = "server=localhost;user=raul;password=Freemind2!;database=budget";

        private static MySqlConnection conn = new MySqlConnection(connectionString);

        public static void CreateTable()
        {
            try
            {
                // Creating Connection  
                // writing sql query  
                MySqlCommand cm = new MySqlCommand("CREATE TABLE IF NOT EXIST budget_data(id int not null, description varchar(255), amount int, type varchar(10)", conn);
                // Opening Connection  
                conn.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Table created Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                conn.Close();
            }
        }


        public void AddRecord(BudgetData data)
        {

            try
            {   
                // Creating Connection  
                // writing sql query  
                MySqlCommand cm = new MySqlCommand($"INSERT INTO {tableName}(description,amount,type,userId) VALUES ('{data.Description}','{data.Amount}','{data.Type}','{data.UserId}')", conn);

                // Opening Connection  
                conn.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Record Inserted Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                conn.Close();

            }

        }

        public BudgetData GetLastItem(int id)
        {
            try
            {
                
                
                MySqlCommand cm =
                    new MySqlCommand($"SELECT  * FROM {tableName} WHERE userId = {id} ORDER BY id DESC LIMIT 1",conn);

                conn.Open();
                
                cm.ExecuteNonQuery();

                MySqlDataReader reader = cm.ExecuteReader();

                BudgetData item = new BudgetData();

                while (reader.Read())
                {
                    item.Id = (int) reader["id"];
                    item.UserId = (int) reader["userId"];
                    item.Amount = (int) reader["amount"];
                    item.Description = (string) reader["description"];
                    item.Type = (string) reader["type"];
                    item.DateCreated = (DateTime) reader["date_created"];
                }

                return item;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<BudgetData> GetAllByUser(int id)
        {
            try
            {
                MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM {tableName} WHERE userId = '{id}'",conn);
                conn.Open();
                

                MySqlDataReader reader = sqlCommand.ExecuteReader();

                List<BudgetData> list = new List<BudgetData>();

                while (reader.Read())
                {
                    BudgetData data = new BudgetData();
                    data.Id = (int)reader["id"];
                    data.UserId = (int)reader["userId"];
                    data.Description = (string)reader["description"];
                    data.Amount = (int)reader["amount"];
                    data.Type = (string)reader["type"];

                    list.Add(data);
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
            finally { 
                conn.Close ();
            }
        }

        public List<BudgetData> GetAll()
        {
            try
            {
                MySqlCommand cm = new MySqlCommand($"SELECT id,userId,description, amount,date_created, type FROM {tableName}", conn);
                conn.Open();

                cm.ExecuteNonQuery();

                MySqlDataReader sqlDataReader = cm.ExecuteReader();

                List<BudgetData> list = new List<BudgetData>();

                while (sqlDataReader.Read())
                {
                    BudgetData budgetData = new BudgetData();
                    budgetData.Id = (int)sqlDataReader["id"];
                    budgetData.DateCreated = (DateTime) sqlDataReader["date_created"];
                    budgetData.UserId = (int)sqlDataReader["userId"];
                    budgetData.Description = (string)sqlDataReader["description"];
                    budgetData.Amount = (int)sqlDataReader["amount"];
                    budgetData.Type = (string)sqlDataReader["type"];
                    list.Add(budgetData);
                }

                return list;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                 throw;
            }
            finally { 
                conn.Close();
            }
        }

        

        public void Update(int id, BudgetData data)
        {
            try
            {
                MySqlCommand cm = new MySqlCommand($"UPDATE {tableName} SET description = {data.Description}, amount = {data.Amount}, type = {data.Description} WHERE id = {id}",conn);
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

        public void Delete(int id)
        {
            try
            {
                MySqlCommand cm = new MySqlCommand($"DELETE FROM {tableName} WHERE id = {id}", conn);

                conn.Open();

                cm.ExecuteNonQuery();

                Console.WriteLine("Record Deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
