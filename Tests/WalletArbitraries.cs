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
        public static Arbitrary<Money> Money() =>
            throw new NotImplementedException();





        public static Arbitrary<WalletOperation> WalletOperation() =>
            throw new NotImplementedException();
    }

}
