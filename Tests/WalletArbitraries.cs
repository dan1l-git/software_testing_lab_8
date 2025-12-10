using FsCheck;
using FsCheck.FSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPropertyTesting.Domain;
using WalletPropertyTesting.Tests.Arbitraries;

namespace WalletPropertyTesting.Tests
{
    public static class WalletArbitraries
    {
        public static Arbitrary<Money> Money() => MoneyGenerator.Money();
        
        public static Arbitrary<WalletOperation> WalletOperation() => OperationGenerator.Operation();
        
        public static Arbitrary<Transaction> Transaction() => TransactionGenerator.Transaction();
    }

}
