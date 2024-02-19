namespace BankConsole;

public class Employee : User , IPerson
{

    public string Department { get; set; }

    public Employee()
    {

    }


    public Employee(int ID, string Name, string Email, decimal Balance,string Department) : base(ID, Name, Email, Balance)
    {
        this.Department = Department;
        setBalance(Balance);
    }


    public override void setBalance(decimal amount)
    {
        base.setBalance(amount);

        
           if(Department.Equals("IT"))
              {
                Balance+= (amount * 0.05m);
              }
        


     

    }

    public override string ShowData()
    {
        return base.ShowData() + $",Departamento:{this.Department}";
    }

    public string GetName()
    {
        return Name + " ?";
    }

    public string GetCountry()
    {
        throw new NotImplementedException();
    }
}
