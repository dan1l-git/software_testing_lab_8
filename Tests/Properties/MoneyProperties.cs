using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPropertyTesting.Domain;

namespace WalletPropertyTesting.Tests.Properties
{

    public class MoneyProperties
    {
        [Property(Arbitrary = new [] {typeof(WalletArbitraries)})]
        public void AdditionIsCommutative(Money a, Money b)
        {
            Assert.Equal((a + b).Amount, (b + a).Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void AdditionIsAssociative(Money a, Money b, Money c)
        {
            Assert.Equal(((a + b) + c).Amount, (a + (b + c)).Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void IdentityElementAddition(Money a)
        {
            var zero = new Money(0);
            Assert.Equal((a + zero).Amount, a.Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void AdditionAndSubtractionAreInverse(Money a, Money b)
        {
            var sum = a + b;
            var result = sum - b;
            Assert.Equal(a.Amount, result.Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void SubtractionThrowsAssumingInsufficientFunds(Money a, Money b)
        {
            if (b.Amount > a.Amount)
            {
                Assert.Throws<InvalidOperationException>(() => a - b);
            }
        }

    }

}
