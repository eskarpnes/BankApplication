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

    public class Bank
    {
        int counter;
        List<Account> accounts;

        public Bank()
        {
            counter = 0;
            accounts = new List<Account>();
        }

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

    public class Money
    {
        double value;

        public Money(double m)
        {
            value = m;
        }

        public double GetValue()
        {
            return value;
        }


        //Overloaders to be able to use and compare Money as numbers.
        public static Money operator +(Money m1, Money m2) =>
            new Money(m1.value + m2.value);

        public static Money operator -(Money m1, Money m2) =>
            new Money(m1.value - m2.value);

        public static bool operator >(Money m1, Money m2) =>
            (m1.value > m2.value);
        
        public static bool operator <(Money m1, Money m2) =>
            (m1.value < m2.value);

        //public static bool operator >(Money m1, int num) =>
        //    (m1.value > num);

        //public static bool operator <(Money m1, int num) =>
        //    (m1.value < num);

        public static bool operator >=(Money m1, Money m2) =>
            (m1.value >= m2.value);

        public static bool operator <=(Money m1, Money m2) =>
            (m1.value >= m2.value);

        public override string ToString() => $"{this.value.ToString()}";

    }
}
