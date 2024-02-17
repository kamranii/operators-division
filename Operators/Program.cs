using System;
using System.Threading.Tasks;

public class Account
{
    private readonly object balanceLock = new object();
    private decimal balance;

    public Account(decimal initialBalance) => balance = initialBalance;

    public decimal Debit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
        }

        decimal appliedAmount = 0;
        lock (balanceLock)
        {
            if (balance >= amount)
            {
                balance -= amount;
                appliedAmount = amount;
            }
        }
        return appliedAmount;
    }

    public void Credit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
        }

        lock (balanceLock)
        {
            balance += amount;
        }
    }

    public decimal GetBalance()
    {
        lock (balanceLock)
        {
            return balance;
        }
    }
}

class AccountTest
{
    static async Task Main()
    {
        #region Debet
        //var account = new Account(1000);
        //var tasks = new Task[100];
        //for (int i = 0; i < tasks.Length; i++)
        //{
        //    tasks[i] = Task.Run(() => Update(account));
        //}
        //await Task.WhenAll(tasks);
        //Console.WriteLine($"Account's balance is {account.GetBalance()}");
        //// Output:
        //// Account's balance is 2000
        ///
        #endregion



        #region DivisionToSeven
        //get inputs
        Console.Write("Dividend: ");
        int dividend = Convert.ToInt32(Console.ReadLine());
        Console.Write("Divisor: ");
        int divisor = Convert.ToInt32(Console.ReadLine());
        int result;
        //get the remainder
        int remainder = dividend % divisor;
        //check if itself can be divided by seven
        if (remainder == 0)
            Console.WriteLine($"{dividend} can be divided by {divisor}");
        else
        {
            //store result
            result = AddToRemainder((int)Math.Abs(remainder), (int)Math.Abs(divisor / 2), (int)Math.Abs(divisor), (int)Math.Abs(dividend));
            if(Math.Sign(remainder) == -1)
                result *= -1;
            //output
            Console.WriteLine(result);
        }
        #endregion
    }
    //process the division operation
    static int AddToRemainder(int remainder, int halfOfDivisor, int divisor, int dividend)
    {
        //check if the remainder is greater than half
        if(remainder > halfOfDivisor)
            return dividend + (divisor - remainder);
        //if less
        else
            return dividend - remainder;
    }








    //static void Update(Account account)
    //{
    //    decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
    //    foreach (var amount in amounts)
    //    {
    //        if (amount >= 0)
    //        {
    //            account.Credit(amount);
    //        }
    //        else
    //        {
    //            account.Debit(Math.Abs(amount));
    //        }
    //    }
    //}

}