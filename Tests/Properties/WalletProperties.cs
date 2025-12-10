using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPropertyTesting.Domain;

namespace WalletPropertyTesting.Tests.Properties
{

    public class WalletProperties
    {
        [Fact]
        public void NewWalletHasZeroBalance()
        {
            var wallet = new Wallet();
            Assert.Equal(0, wallet.Balance.Amount);
            Assert.Empty(wallet.History);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void DepositIncreasesBalance(Money amount)
        {
            var wallet = new Wallet();
            var initialBalance = wallet.Balance;

            wallet.Deposit(amount);

            Assert.Equal((initialBalance + amount).Amount, wallet.Balance.Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void WithdrawDecreasesBalance(Money amount)
        {
            var wallet = new Wallet();
            wallet.Deposit(amount); 
            var balanceAfterDeposit = wallet.Balance;

            wallet.Withdraw(amount);

            Assert.Equal((balanceAfterDeposit - amount).Amount, wallet.Balance.Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void OperationsAddHistoryRecord(Money amount)
        {
            var wallet = new Wallet();
            wallet.Deposit(amount);
            
            Assert.Single(wallet.History);
            Assert.Equal(TransactionType.Deposit, wallet.History.First().Type);
            Assert.Equal(amount.Amount, wallet.History.First().Amount.Amount);
        }
        
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void BalanceEqualsSumOfHistory(List<Money> deposits)
        {
            var wallet = new Wallet();
            
            foreach (var money in deposits)
            {
                wallet.Deposit(money);
            }

            var expectedBalance = deposits.Sum(x => x.Amount);
            var historySum = wallet.History
                .Where(t => t.Type == TransactionType.Deposit)
                .Sum(t => t.Amount.Amount);

            Assert.Equal(expectedBalance, wallet.Balance.Amount);
            Assert.Equal(expectedBalance, historySum);
        }

    }

}
