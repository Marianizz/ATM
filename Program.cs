using System;
using System.Collections.Generic;
using System.Linq;

public class CardHolder
{
    private string cardNum;
    private int pin;
    private string firstName;
    private string lastName;
    private double balance;

    // Constructor
    public CardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    // Proprietăți
    public string CardNum
    {
        get { return cardNum; }
        set { cardNum = value; }
    }

    public int Pin
    {
        get { return pin; }
        set { pin = value; }
    }

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    // Metode pentru operații bancare
    public void Deposit(double amount)
    {
        balance += amount;
    }

    public bool Withdraw(double amount)
    {
        if (balance >= amount)
        {
            balance -= amount;
            return true; // Retragerea a fost efectuată cu succes
        }
        return false; // Fonduri insuficiente
    }

    // Metoda pentru afișarea soldului
    public void DisplayBalance()
    {
        Console.WriteLine("Soldul curent: " + balance);
    }
}

public class ATM
{
    // Metoda pentru afișarea opțiunilor disponibile
    private static void PrintOptions()
    {
        Console.WriteLine("Vă rugăm să alegeți o opțiune:");
        Console.WriteLine("1. Depunere");
        Console.WriteLine("2. Retragere");
        Console.WriteLine("3. Vizualizare sold");
        Console.WriteLine("4. Ieșire");
    }

    public static void Main(string[] args)
    {
        List<CardHolder> cardHolders = new List<CardHolder>()
        {
            new CardHolder("12345678901234", 1234, "John", "Griffith", 150.31),
            new CardHolder("13456781212223", 2221, "Andreea", "Jones", 322.31),
            new CardHolder("22122212111232", 2112, "Silviu", "Mici", 987.31),
            new CardHolder("91222222211322", 9223, "Marian", "Smith", 2.31)
        };

        Console.WriteLine("Bine ați venit la ATM-ul nostru!");
        Console.WriteLine("Vă rugăm să introduceți numărul cardului:");

        string inputCardNum = Console.ReadLine();
        CardHolder currentUser = cardHolders.FirstOrDefault(c => c.CardNum == inputCardNum);

        if (currentUser == null)
        {
            Console.WriteLine("Cardul nu este recunoscut. Vă rugăm să încercați din nou.");
            return;
        }

        Console.WriteLine("Vă rugăm să introduceți PIN-ul:");

        int inputPin;
        if (!int.TryParse(Console.ReadLine(), out inputPin) || currentUser.Pin != inputPin)
        {
            Console.WriteLine("PIN incorect. Vă rugăm să încercați din nou.");
            return;
        }

        Console.WriteLine("Bine ați venit, " + currentUser.FirstName + "!");

        int option;
        do
        {
            PrintOptions();
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Opțiune invalidă. Vă rugăm să introduceți o opțiune validă.");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Ce sumă doriți să depozitați?");
                    double depositAmount;
                    if (!double.TryParse(Console.ReadLine(), out depositAmount) || depositAmount <= 0)
                    {
                        Console.WriteLine("Sumă invalidă.");
                        continue;
                    }
                    currentUser.Deposit(depositAmount);
                    Console.WriteLine("Depunere efectuată cu succes.");
                    break;
                case 2:
                    Console.WriteLine("Ce sumă doriți să retrageți?");
                    double withdrawAmount;
                    if (!double.TryParse(Console.ReadLine(), out withdrawAmount) || withdrawAmount <= 0)
                    {
                        Console.WriteLine("Sumă invalidă.");
                        continue;
                    }
                    if (currentUser.Withdraw(withdrawAmount))
                    {
                        Console.WriteLine("Retragere efectuată cu succes.");
                    }
                    else
                    {
                        Console.WriteLine("Fonduri insuficiente.");
                    }
                    break;
                case 3:
                    currentUser.DisplayBalance();
                    break;
                case 4:
                    Console.WriteLine("Vă mulțumim! O zi frumoasă!");
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă. Vă rugăm să introduceți o opțiune validă.");
                    break;
            }
        }
        while (option != 4);
    }
}
