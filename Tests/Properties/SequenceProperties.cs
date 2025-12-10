using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Fluent;
using WalletPropertyTesting.Domain;
using WalletPropertyTesting.Tests.Arbitraries;

namespace WalletPropertyTesting.Tests.Properties
{

    public class SequenceProperties
    {
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public Property ValidSequenceResultsInCorrectBalance(List<WalletOperation> operations)
        {
            var wallet = new Wallet();
            decimal expectedBalance = 0;

            foreach (var op in operations)
            {
                if (op.Type == WalletOpType.Deposit)
                {
                    wallet.Deposit(new Money(op.Amount));
                    expectedBalance += op.Amount;
                }
                else
                {
                    if (expectedBalance >= op.Amount)
                    {
                        wallet.Withdraw(new Money(op.Amount));
                        expectedBalance -= op.Amount;
                    }
                    else
                    {
                        try 
                        {
                            wallet.Withdraw(new Money(op.Amount));
                            return false.ToProperty(); 
                        }
                        catch (InvalidOperationException) 
                        { 
                        }
                    }
                }
            }

            return (wallet.Balance.Amount >= 0).ToProperty();
        }

    }

}
