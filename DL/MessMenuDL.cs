using MySql.Data.MySqlClient;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class MessMenuDL
    {
        public DataTable GetAllMenu()
        {
            string query = "SELECT MenuID, DayName, MealType, FoodItem FROM messmenu ORDER BY MenuID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetMenuById(int menuID)
        {
            string query = "SELECT * FROM messmenu WHERE MenuID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", menuID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertMenu(MessMenuModel menu)
        {
            string query = @"INSERT INTO messmenu (DayName, MealType, FoodItem) 
                             VALUES (@day, @meal, @food)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@day", menu.DayName),
                new MySqlParameter("@meal", menu.MealType),
                new MySqlParameter("@food", menu.FoodItem)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateMenu(MessMenuModel menu)
        {
            string query = @"UPDATE messmenu SET 
                             DayName = @day,
                             MealType = @meal,
                             FoodItem = @food
                             WHERE MenuID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@day", menu.DayName),
                new MySqlParameter("@meal", menu.MealType),
                new MySqlParameter("@food", menu.FoodItem),
                new MySqlParameter("@id", menu.MenuID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteMenu(int menuID)
        {
            string query = "DELETE FROM messmenu WHERE MenuID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", menuID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}