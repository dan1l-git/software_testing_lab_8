using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck.Fluent;
using WalletPropertyTesting.Domain;

namespace WalletPropertyTesting.Tests.Arbitraries
{
    public static class TransactionGenerator
    {
        public static Arbitrary<Transaction> Transaction()
        {
            var genMoney = MoneyGenerator.Money().Generator;
            
            var genTransaction = Gen.OneOf(
                genMoney.Select(m => Domain.Transaction.Deposit(m)),
                genMoney.Select(m => Domain.Transaction.Withdraw(m))
            );

            return Arb.From(genTransaction);

        }
    }

}
