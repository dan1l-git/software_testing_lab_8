using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPropertyTesting.Domain;
using FsCheck.Fluent;

namespace WalletPropertyTesting.Tests.Arbitraries
{
    public static class MoneyGenerator
    {
        public static Arbitrary<Money> Money()
        {
            var gen = Gen.Choose(0, 1000000)
                .Select(i => (decimal)i + (decimal)new Random().NextDouble()) // Додаємо копійки
                .Select(d => new Money(Math.Round(d, 2))); // Округляємо до 2 знаків

            return Arb.From(gen);
        }
    }

}
