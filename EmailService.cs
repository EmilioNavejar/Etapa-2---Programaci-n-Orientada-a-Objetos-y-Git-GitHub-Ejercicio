using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService
{

    public static void SendEmail()
    {
        var messages= new MimeMessage();

        messages.From.Add(new MailboxAddress("Emilio Navejar","emilio.navejacr@uanl.edu.mx"));
        messages.To.Add(new MailboxAddress("Emilio Navejar","jesustigres58@gmail.com"));
        messages.Subject="Usuarios nuevos";

        messages.Body=new TextPart("plain")
        {
            Text=GetEmailText()
        };

        using(var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com",587,false);
            client.Authenticate("jesustigres58@gmail.com","bhvyoymbopnbzrht");
            client.Send(messages);
            client.Disconnect(true);
        }
    }

    private static string GetEmailText()
    {
        List<User> newUsers = Storage.GetNewUsers();

        if(newUsers.Count == 0)
        {
            return "No hay usuarios nuevos";
        }

        string emailText = "Usuario agregados hoy:\n";

        foreach(User user in newUsers)
        {
            emailText += "\t+ " + user.ShowData() + "\n";
        }

        return emailText;

    }



}