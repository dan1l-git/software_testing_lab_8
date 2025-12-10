using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPropertyTesting.Domain;

namespace WalletPropertyTesting.Tests.Properties
{

    public class RepositoryProperties
    {
        [Property(Arbitrary = new[] { typeof(WalletArbitraries) })]
        public void SaveAndGetReturnsSameWalletState(Money initialDeposit)
        {
            var repo = new InMemoryWalletRepository();
            var service = new WalletService(repo);
            
            var wallet = service.CreateWallet();
            service.Deposit(wallet.Id, initialDeposit.Amount);

            var retrievedWallet = repo.Get(wallet.Id);

            Assert.Equal(wallet.Id, retrievedWallet.Id);
            Assert.Equal(wallet.Balance.Amount, retrievedWallet.Balance.Amount);
        }
        
        [Fact] 
        public void GetNonExistentWalletThrows()
        {
            var repo = new InMemoryWalletRepository();
            Assert.Throws<KeyNotFoundException>(() => repo.Get(Guid.NewGuid()));
        }
    }
}
