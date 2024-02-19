using System.Text.RegularExpressions;
using BankConsole;


if (args.Length == 0)
{
    EmailService.SendEmail();
    
}else{
    ShowMenu();
}

void ShowMenu()
{

    Console.Clear();
    Console.WriteLine("Seleccione una opcion:");
    Console.WriteLine("1.- Crear usuario");
    Console.WriteLine("2.- Elimina un usuario existente");
    Console.WriteLine("3.- Salir");


    int option=0;

    do
    {
        string input=Console.ReadLine();
        if(!int.TryParse(input,out option))
        {
            Console.WriteLine("Debes ingresar un numero (1,2,3)");
        }else if(option>3)
        {
            Console.WriteLine("Debes ingresar un numero (1,2,3)");
        }
            
        
    } while (option==0||option>3);


    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
        
        
    }
}

void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa la informacion del usuario");

    int ID;

    bool ValidID = false;
    do
    {
        Console.WriteLine("ID:");
        string idInput = Console.ReadLine();
        ValidID = int.TryParse(idInput, out ID) && ID > 0;

        if (!ValidID)
        {
            Console.WriteLine("Debes de ingresar un ID valido (numero entero positivo)");
        }
    } while (!ValidID);

    if (Storage.IsIdAlreadyUsed(ID))
    {
        Console.WriteLine("¡El ID ingresado ya está en uso! Por favor, ingrese un ID único.");
        Thread.Sleep(5000);
        ShowMenu();
        
    }

    Console.WriteLine("Nombre:");
    string name = Console.ReadLine();

    string email;

    bool ValidEmail = false;

    do
    {
        Console.WriteLine("Correo:");
        email = Console.ReadLine();
        ValidEmail=IsValidEmail(email);
        if(!ValidEmail)
        {
            Console.WriteLine("Debes ingresar un correo valido");
        }
        
    } while (!ValidEmail);

     decimal balance;
     bool validBalance = false;
    do
    {
        Console.WriteLine("Saldo:");
        string balanceInput = Console.ReadLine();
        validBalance = decimal.TryParse(balanceInput, out balance) && balance >= 0;
        if (!validBalance)
        {
            Console.WriteLine("El saldo ingresado no es válido,ingrese un saldo decimal positivo.");
        }
    } while (!validBalance);

     char userType=' ';
     bool validUserType = false;
     do
     {
        Console.WriteLine("Escriba 'c' si el usuario es cliente, 'e' si es Empleado");
        string userTypeInput = Console.ReadLine();
        validUserType=userTypeInput.Length==1 && (userTypeInput[0]=='c' || userTypeInput[0]=='e');
        if(!validUserType)
        {
            Console.WriteLine("Debes ingresar 'c' o 'e'");
        }
        else
        {
            userType=userTypeInput[0];
        }
        
     } while (!validUserType);

    User newUser;

    if (userType == 'c')
    {
        Console.WriteLine("Regimen Fiscal:");
        char taxRegime = char.Parse(Console.ReadLine());
        newUser = new Client(ID, name, email, balance, taxRegime);
    }
    else
    {
        Console.WriteLine("Departamento:");
        string department = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, department);
    }

    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado");
    Thread.Sleep(2000);
    ShowMenu();



    static bool IsValidEmail(string email)
    {
      string pattern=@"[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,5}";

      Regex regex=new Regex(pattern);

      return regex.IsMatch(email);
    }
}


void DeleteUser()
{
   Console.Clear();

    int ID;
    bool validID = false;
    do
    {
        Console.WriteLine("Ingresa el ID del usuario a eliminar:");
        string idInput = Console.ReadLine();
        validID = int.TryParse(idInput, out ID) && ID > 0;

        if (!validID)
        {
            Console.WriteLine("El ID ingresado no es válido. Debe ser un entero positivo.");
        }else{
            if(!Storage.UserExists(ID))
            {
                Console.WriteLine("El ID ingresado no existe");
                validID=false;
            }
        }
    } while (!validID);

    string result = Storage.DeleteUser(ID);

    if (result.Equals("Success"))
    {
        Console.WriteLine("Usuario eliminado");
        Thread.Sleep(2000);
        ShowMenu();
    }
}




