using FsCheck;
using FsCheck.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPropertyTesting.Tests.Arbitraries
{
    public enum WalletOpType { Deposit, Withdraw }

    public record WalletOperation(WalletOpType Type, decimal Amount);

    public static class OperationGenerator
    {
        public static Arbitrary<WalletOperation> Operation()
        {
            var genMoney = MoneyGenerator.Money().Generator;
            var genType = Gen.Elements(WalletOpType.Deposit, WalletOpType.Withdraw);

            var genOperation = 
                from m in genMoney
                from t in genType
                select new WalletOperation(t, m.Amount);

            return Arb.From(genOperation);

        }
    }

}
