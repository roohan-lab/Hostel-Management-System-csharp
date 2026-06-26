using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class MessMenuBL
    {
        private MessMenuDL menuDL = new MessMenuDL();

        private string ValidateMenu(MessMenuModel menu)
        {
            if (string.IsNullOrEmpty(menu.DayName))
                return "Day Name is required!";

            if (string.IsNullOrEmpty(menu.MealType))
                return "Meal Type is required!";

            if (string.IsNullOrEmpty(menu.FoodItem))
                return "Food Item is required!";

            return "VALID";
        }

        public string SaveMenu(MessMenuModel menu)
        {
            string validation = ValidateMenu(menu);
            if (validation != "VALID")
                return validation;

            bool saved = menuDL.InsertMenu(menu);
            return saved ? "SUCCESS" : "Failed to save menu!";
        }

        public string UpdateMenu(MessMenuModel menu)
        {
            if (menu.MenuID <= 0)
                return "Invalid Menu ID!";

            string validation = ValidateMenu(menu);
            if (validation != "VALID")
                return validation;

            bool updated = menuDL.UpdateMenu(menu);
            return updated ? "SUCCESS" : "Failed to update menu!";
        }

        public string DeleteMenu(int menuID)
        {
            if (menuID <= 0)
                return "Invalid Menu ID!";

            bool deleted = menuDL.DeleteMenu(menuID);
            return deleted ? "SUCCESS" : "Failed to delete menu!";
        }

        public DataTable GetAllMenu()
        {
            return menuDL.GetAllMenu();
        }

        public DataTable GetMenuById(int menuID)
        {
            return menuDL.GetMenuById(menuID);
        }
    }
}