using System;
using BankApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTest
{
    [TestClass]
    public class TestBank
    {
        Bank bank;
        Person erlend;
        Account erlendAccount;

        public void SetupTest()
        {
            this.bank = new Bank();
            this.erlend = new Person("Erlend", 1995);
            erlend.AddCash(new Money(50));
            this.erlendAccount = bank.CreateAccount(erlend, new Money(50));
        }

        [TestMethod]
        public void TestCreateAccount()
        {
            //Setup
            SetupTest();
            string expectedName = "erlend0";
            double expectedBalance = 50;

            //Assert
            string actualName = this.erlendAccount.GetName();
            double actualBalance = this.erlendAccount.GetBalance().GetValue();
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [TestMethod]
        public void TestCreateMultipleAccounts()
        {
            //Setup
            SetupTest();
            erlend.AddCash(new Money(100));
            Account account2 = bank.CreateAccount(erlend, new Money(100));
            Person maria = new Person("Maria", 1995);
            maria.AddCash(new Money(150));
            Account account3 = bank.CreateAccount(maria, new Money(150));
            Account[] erlendAccounts = bank.GetAccountsForCustomer(erlend);

            //Assert
            string[] expectedNames = new string[3] { "erlend0", "erlend1", "maria2" };
            string[] actualNames = new string[3] { erlendAccount.GetName(), account2.GetName(), account3.GetName() };
            Person expectedCustomer = erlend;
            Person[] actualCustomer = new Person[2] { erlendAccounts[0].GetOwner(), erlendAccounts[1].GetOwner() };
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expectedNames[i], actualNames[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(expectedCustomer, actualCustomer[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeposit_WhenAmountIsTooMuch()
        {
            //Setup
            SetupTest();
            erlend.AddCash(new Money(100));

            //Act
            bank.Deposit(erlendAccount, new Money(200));
        }

        [TestMethod]
        public void TestDeposit()
        {
            //Setup
            SetupTest();
            erlend.AddCash(new Money(100));
            bank.Deposit(erlendAccount, new Money(100));

            //Assert
            double expectedBalance = 150;
            double actualBalance = erlendAccount.GetBalance().GetValue();
            double expectedCash = 0;
            double actualCash = erlend.GetCash().GetValue();

            Assert.AreEqual(expectedBalance, actualBalance);
            Assert.AreEqual(expectedCash, actualCash);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWithdraw_WhenAmountIsTooMuch()
        {
            //Setup
            SetupTest();

            //Act
            bank.Withdraw(erlendAccount, new Money(200));
        }

        [TestMethod]
        public void TestWithdraw()
        {
            //Setup
            SetupTest();
            bank.Withdraw(erlendAccount, new Money(50));

            //Assert
            double expectedBalance = 0;
            double actualBalance = erlendAccount.GetBalance().GetValue();
            double expectedCash = 50;
            double actualCash = erlend.GetCash().GetValue();

            Assert.AreEqual(expectedBalance, actualBalance);
            Assert.AreEqual(expectedCash, actualCash);
        }

        [TestMethod]
        public void TestTransfer()
        {
            //Setup
            SetupTest();
            Person maria = new Person("Maria", 1995);
            maria.AddCash(new Money(100));
            Account mariaAccount = bank.CreateAccount(maria, new Money(100));
            bank.Transfer(erlendAccount, mariaAccount, new Money(50));

            //Assert
            double expectedMariaBalance = 150;
            double expectedErlendBalance = 0;
            double actualMariaBalance = mariaAccount.GetBalance().GetValue();
            double actualErlendBalance = erlendAccount.GetBalance().GetValue();

            Assert.AreEqual(expectedMariaBalance, actualMariaBalance);
            Assert.AreEqual(expectedErlendBalance, actualErlendBalance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTransfer_WhenAmountIsTooMuch()
        {
            //Setup
            SetupTest();
            Person maria = new Person("Maria", 1995);
            Account mariaAccount = bank.CreateAccount(maria, new Money(100));

            //Act
            bank.Transfer(erlendAccount, mariaAccount, new Money(100));
        }

        [TestMethod]
        public void TestMoney()
        {
            //Setup
            Money moneyDouble = new Money(50.0);
            Money moneyInt = new Money(50);
            Money moneyString = new Money("50");

            Money expectedMoney = new Money(50.0);

            //Assert
            Assert.AreEqual(expectedMoney.GetValue(), moneyDouble.GetValue());
            Assert.AreEqual(expectedMoney.GetValue(), moneyInt.GetValue());
            Assert.AreEqual(expectedMoney.GetValue(), moneyString.GetValue());
        }
    }
}
