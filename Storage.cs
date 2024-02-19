using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BankConsole
{

    public static class Storage
    {
        static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

        public static void AddUser(User user)
        {

            if (IsIdAlreadyUsed(user.ID))
            {
                Console.WriteLine("¡El ID ingresado ya está en uso! Por favor, ingrese un ID único.");
                Thread.Sleep(4000);
                return;
            }

            string json = "", usersInFile = "";

            if (File.Exists(filePath))
            {
                usersInFile = File.ReadAllText(filePath);
            }

            var listUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile);

            if (listUsers == null)
            {
                listUsers = new List<object>();
            }

            listUsers.Add(user);

            JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };

            json = JsonConvert.SerializeObject(listUsers);

            File.WriteAllText(filePath, json);
        }

        public static bool IsIdAlreadyUsed(int id)
        {
            string usersInFile = "";

            if (File.Exists(filePath))
            {
                usersInFile = File.ReadAllText(filePath);
            }

            var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

            if (listObjects == null)
            {
                return false;
            }

            foreach (object obj in listObjects)
            {
                JObject user = (JObject)obj;
                int userId = user.ContainsKey("ID") ? user["ID"].Value<int>() : -1;

                if (userId == id)
                {
                    return true; 
                }
            }

            return false; 
        }

        public static List<User> GetNewUsers()
        {
            string usersInFile = "";

            var listUsers = new List<User>();

            if (File.Exists(filePath))
            {
                usersInFile = File.ReadAllText(filePath);
            }

            var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

            if (listUsers == null)
            {
                return listUsers;
            }

            foreach (object obj in listObjects)
            {
                User newUser;
                JObject user = (JObject)obj;

                if (user.ContainsKey("TaxRegime"))
                {
                    newUser = user.ToObject<Client>();
                }
                else
                {
                    newUser = user.ToObject<Employee>();
                }

                listUsers.Add(newUser);
            }

            var newUserList = listUsers.Where(user => user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();

            return newUserList;

        }


        public static string DeleteUser(int ID)
        {
            string usersInFile = "";
            var listUsers = new List<User>();

            if (File.Exists(filePath))
            {
                usersInFile = File.ReadAllText(filePath);
            }

            var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

            if (listObjects == null)
            {
                return "No users found";
            }

            foreach (object obj in listObjects)
            {
                User newUser;
                JObject user = (JObject)obj;

                if (user.ContainsKey("TaxRegime"))
                {
                    newUser = user.ToObject<Client>();
                }
                else
                {
                    newUser = user.ToObject<Employee>();
                }

                listUsers.Add(newUser);
            }

            var userToDelete = listUsers.Where(user => user.GetID() == ID).Single();

            listUsers.Remove(userToDelete);

            JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };

            string json = JsonConvert.SerializeObject(listUsers, settings);

            File.WriteAllText(filePath, json);

            return "Success";
        }



        public static bool UserExists(int ID)
        {
            string usersInFile = "";
            if (File.Exists(filePath))
            {
                usersInFile = File.ReadAllText(filePath);
            }

            var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

            if (listObjects == null)
            {
                return false;
            }

            foreach (object obj in listObjects)
            {
                JObject user = (JObject)obj;
                if (user.ContainsKey("ID"))
                {
                    int userID = user["ID"].ToObject<int>();
                    if (userID == ID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
 
    }




}
