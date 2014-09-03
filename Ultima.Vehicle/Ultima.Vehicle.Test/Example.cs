/*
  public class AccountSpecifications
   {
       public Specification when_constructing_an_account = new
ConstructorSpecification<Account>()
       {
           When = () => new Account("Jane Smith", 17),
           Expect =
               {
                   account => account.AccountHolderName == "Jane Smith",
                   account => account.UniqueIdentifier == 17,
                   account => account.CurrentBalance == new Money(0m),
                   account => account.Transactions.Count() == 0
               }
       };
       public Specification when_depositing_to_a_new_account = new ActionSpecification<Account>()
       {
           Before = () =>SystemTime.Set(new DateTime(2011,1,1)),
           On = () => new Account("Joe User", 14),
           When = account => account.Deposit(new Money(50)),
           Expect =
               {
                   account => account.CurrentBalance == new Money(50),
                   account => account.Transactions.Count() == 1,
                   account => account.Transactions.First().Amount ==
new Money(50),
                   account => account.Transactions.First().Type ==
TransactionType.Deposit,
                   account => account.Transactions.First().Timestamp
== new DateTime(2011,1,1),
               },
           Finally = SystemTime.Clear
       };
       public Specification when_withdrawing_to_overdraw_an_account = new FailingSpecification<Account, CannotOverdrawAccountException>()
       {
           On = () => new Account("Joe User", 14),
           When = account => account.Withdraw(new Money(50)),
           Expect =
               {
                   exception => exception.Message == "The operation
would overdraw the account"
               }
       };
       public Specification
when_witdrawing_from_account_with_sufficient_funds = new ActionSpecification<Account>()
       {
           Before = () => SystemTime.Set(new DateTime(2011, 1, 1)),
           On = () => new Account("Joe User", 14, new Money(100)),
           When = account => account.Withdraw(new Money(50)),
           Expect =
               {
                   account => account.CurrentBalance == new Money(50),
                   account => account.Transactions.Count() == 1,
                   account => account.Transactions.First().Amount ==
new Money(-50),
                   account => account.Transactions.First().Type ==
TransactionType.Deposit,
               },
           Finally = SystemTime.Clear
       };
   }
 
RESULTS in:
 
when constructing an account - Passed
 
On:
 
 
 
Results with:
Account: 17 owned by Jane Smith with a balance $0
       With no previous transactions.
 
Expectations:
       The Account's Account Holder Name must be equal to "Jane  Smith" Passed
 
       The Account's Unique Identifier must be equal to 17 Passed
       The Account's Current Balance must be equal to $0 Passed
       The Account's Transactions Count() must be equal to 0 Passed
--------------------------------------------------------------------------------
 
 
when depositing to a new account - Passed
 
On:
Account: 14 owned by Joe User with a balance $0
       With no previous transactions.
 
 
Results with:
Account: 14 owned by Joe User with a balance $50
       Deposit for $50 at 1/1/2011 12:00:00 AM
 
 
Expectations:
       The Account's Current Balance must be equal to $50 Passed
       The Account's Transactions Count() must be equal to 1 Passed
       The Account's Transactions First() Amount must be equal to $50 Passed
       (int)(The Account's Transactions First() Type) must be equal to 1 Passed
 
       The Account's Transactions First() Timestamp must be equal to 1/1/2011 1
2:00:00  A M Passed
--------------------------------------------------------------------------------
 
 
when withdrawing to overdraw an account - Passed
 
On:
Account: 14 owned by Joe User with a balance $0
       With no previous transactions.
 
 
Results with:
DocGeneratorExample.CannotOverdrawAccountException
The operation would overdraw the account
 
Expectations:
       The Cannot Overdraw Account Exception's Message must be equal to "The o
peration would overdraw the account" Passed
--------------------------------------------------------------------------------
 
 
when witdrawing from account with sufficient funds - Passed
 
On:
Account: 14 owned by Joe User with a balance $100
       With no previous transactions.
 
 
Results with:
Account: 14 owned by Joe User with a balance $50
       Deposit for $-50 at 1/1/2011 12:00:00 AM
 
 
Expectations:
       The Account's Current Balance must be equal to $50 Passed
       The Account's Transactions Count() must be equal to 1 Passed
       The Account's Transactions First() Amount must be equal to $-50 Passed
       (int)(The Account's Transactions First() Type) must be equal to 1 Passed
 
--------------------------------------------------------------------------------
 
 
Press any key to continue . . .
 */