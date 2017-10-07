## BankApplication
This is a standard BankApplication written in C# for an assignment in a recruitment process.

The bank needed 5 functions:

#Account CreateAccount(Person customer, Money initialDeposit)
Should create a new Account for the customer with a given balance. The name of the account should be the name of the customer + a number starting from 0 and counting upwards.
Should throw an error if the Person does not have the money needed for the deposit.

#Account[] GetAccountsForCustomer(Person customer)
Should return every account for the given customer.

#void Deposit(Account to, Money amount)
Should deposit money from a person to his/her account. Should throw an error if the person does not have enough money.

#void Withdraw(Account from, Money amount)
Should withdraw money from a persons account to the person. Should throw an error if it's not enough money in the account.

#void Transfer(Account from, Account to, Money amount)
Should transfer money from an account to another. Should throw an error if it's not enough money in the account it is sending from.

##The classes
#Bank
The bank class has all the methods listed above, and a counter to name the accounts opened in the bank, and a list of every account in the bank.

#Person
The person class has the name of the person, the birthyear (not needed for the task) and a variable for how much cash the person has. This is the cash that would be deposited and withdrawn from the persons accounts.

#Account
The account class has the name of the account, the owner of the account (as a Person object) and the balance.

#Money
The money class is somewhat special in this project. In the scope of the project, the money class could as well be represented by a double. However, in the name of OO-programming, it has it's own class. 
If the scope of the class were to be bigger, such a class could contain methods to convert the money between different currencies, or have constructors for every type of parameter (ints, doubles, strings etc).

##Testing
To test this project I have included a unit test that tests the Bank class for every action asked for in the scenario. The application passed all tests.


