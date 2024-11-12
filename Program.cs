using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Podaj email:");
        string email = Console.ReadLine();
        Console.WriteLine("Podaj tekst wiadomości:");
        string text = Console.ReadLine();
        Mail m = new Mail();
        try
        {
            m.Send(email, text);
        }
        catch (InvalidMailException e)
        {
            Console.WriteLine("Podano nieprawidłowy mail: " + e.Message);
        }
    }
}

public class Mail
{
    public void Send(string email, string text)
    {
        if (!IsValidEmail(email))
        {
            throw new InvalidMailException($"Nieprawidłowy format emaila: {email}");
        }
        
        Console.WriteLine($"Do: {email}\n{text}");
    }

    private bool IsValidEmail(string email)
    {
        int atIndex = -1;
        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == '@')
            {
                if (atIndex != -1)
                {
                    return false; 
                }
                atIndex = i;
            }
        }

        if (atIndex <= 0 || atIndex >= email.Length - 1)
        {
            return false;
        }

        int dotIndex = -1;
        for (int i = atIndex + 1; i < email.Length; i++)
        {
            if (email[i] == '.')
            {
                dotIndex = i;
                break;
            }
        }

        if (dotIndex <= atIndex + 1 || dotIndex >= email.Length - 1)
        {
            return false;
        }

        return true;
    }
}

public class InvalidMailException : Exception
{
    public InvalidMailException(string message) : base(message) { }
}
