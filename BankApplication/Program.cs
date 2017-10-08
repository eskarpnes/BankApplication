using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the project made for a software company as an application task. It is tested with unit tests, and the console program does nothing at the moment");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The bank class. This class contains every function required for the task.
    /// </summary>
    public class Bank
    {
        int counter;
        List<Account> accounts;

        public Bank()
        {
            counter = 0;
            accounts = new List<Account>();
        }

        /// <summary>
        /// A function that creates a new account for a customer, returns it, and adds it to the bank.
        /// </summary>
        /// <param name="customer">The customer that is creating an account</param>
        /// <param name="initialDeposit">The initial deposit. This is taken from the customers cash</param>
        /// <returns>A new account object</returns>
        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            String account_name = customer.GetName().ToLower() + this.counter.ToString();
            if (initialDeposit > new Money(0)) {
                Account account = new Account(account_name, initialDeposit, customer);
                counter++;
                customer.RemoveCash(initialDeposit);
                Console.WriteLine("New account made for " + customer.GetName());
                accounts.Add(account);
                return account;
            }
            throw new System.ArgumentException("Initial deposit is too small");
        }

        /// <summary>
        /// A function that returns every account the customer owns.
        /// </summary>
        /// <param name="customer">The customer it shall return the accounts for.</param>
        /// <returns>A list of accounts the customer owns.</returns>
        public Account[] GetAccountsForCustomer(Person customer)
        {
            List<Account> output = new List<Account>();
            foreach (Account account in accounts)
            {
                if (account.GetOwner().Equals(customer))
                {
                    output.Add(account);
                }
            }
            return output.ToArray();
        }

        /// <summary>
        /// A function that deposits money from a persons cash to his account.
        /// </summary>
        /// <param name="to">The account to deposit to</param>
        /// <param name="amount">The amount to deposit</param>
        public void Deposit(Account to, Money amount)
        {
            Person owner = to.GetOwner();
            Money cash = owner.GetCash();
            if (cash >= amount)
            {
                to.Deposit(amount);
                owner.RemoveCash(amount);
                Console.WriteLine("Succsessfully deposited " + amount.ToString());
            }
            else
            {
                throw new System.ArgumentException("Not enough money to deposit");
            }
        }

        /// <summary>
        /// A function that withdraws money and adds it to the owners cash.
        /// </summary>
        /// <param name="from">The account to withdraw from</param>
        /// <param name="amount">The amount of money to withdraw</param>
        public void Withdraw(Account from, Money amount)
        {
            if (from.GetBalance() >= amount)
            {
                from.GetOwner().AddCash(amount);
                from.Withdraw(amount);
                Console.WriteLine("Succsessfully withdrew " + amount.ToString());
            }
            else
            {
                throw new System.ArgumentException("Not enough money to withdraw");
            }
        }

        /// <summary>
        /// A function that transfers money from one account to another.
        /// </summary>
        /// <param name="from">The account to transfer from</param>
        /// <param name="to">The account to transfer to</param>
        /// <param name="amount">The amount to transfer</param>
        public void Transfer(Account from, Account to, Money amount)
        {
            if (from.GetBalance() >= amount)
            {
                from.Withdraw(amount);
                to.Deposit(amount);
            }
            else
            {
                throw new System.ArgumentException("Not enough money for a transfer.");
            }
        }

    }

    public class Person
    {
        private string name;
        private int birthyear;
        private Money cash;

        public Person(string name, int birthyear)
        {
            this.name = name;
            this.birthyear = birthyear;
            this.cash = new Money(0);
        }

        public void AddCash(Money amount)
        {
            cash += amount;
        }

        public void RemoveCash(Money amount)
        {
            if (cash >= amount)
            {
                cash -= amount;
            }
            else
            {
                throw new ArgumentException("This person does not have enough cash!");
            }
        }

        public string GetName()
        {
            return name;
        }

        public Money GetCash()
        {
            return cash;
        }
    }

    public class Account
    {
        string account_name;
        Person owner;
        Money balance;

        public Account(string account_name, Money deposit, Person owner)
        {
            this.account_name = account_name;
            balance = deposit;
            this.owner = owner;
        }

        public Person GetOwner()
        {
            return owner;
        }

        public Money GetBalance()
        {
            return balance;
        }

        public string GetName()
        {
            return account_name;
        }

        public void Deposit(Money amount)
        {
            balance += amount;
        }

        public void Withdraw(Money amount)
        {
            balance -= amount;
        }
    }

    /// <summary>
    /// This can be constructed with a double, int, or string (e.g "50") and saves the value of the money.
    /// This class could be useful if you wanted to support multiple currencies.
    /// The class can be used as a double with operators like addition and subtracton, and the comparison operators.
    /// </summary>
    public class Money
    {
        double value;

        public Money(double m)
        {
            value = m;
        }

        public Money(string m)
        {
            value = double.Parse(m);
        }

        public Money(int m)
        {
            value = Convert.ToDouble(m);
        }

        public double GetValue()
        {
            return value;
        }


        //Overloaders to be able to use and compare Money as doubles.
        public static Money operator +(Money m1, Money m2) =>
            new Money(m1.value + m2.value);

        public static Money operator -(Money m1, Money m2) =>
            new Money(m1.value - m2.value);

        public static bool operator >(Money m1, Money m2) =>
            (m1.value > m2.value);
        
        public static bool operator <(Money m1, Money m2) =>
            (m1.value < m2.value);

        public static bool operator >=(Money m1, Money m2) =>
            (m1.value >= m2.value);

        public static bool operator <=(Money m1, Money m2) =>
            (m1.value >= m2.value);

        public override string ToString() => $"{this.value.ToString()}";

    }
}
