using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Raymaker.FactoryDemo.Console.CreditLimitProviders
{
    public class CreditLimitProviderFactory
    {
        private readonly IReadOnlyDictionary<string, ICreditLimitProvider> providers;

        public CreditLimitProviderFactory(IUserCreditService userCreditService)
        {
            var creditLimitProviderType = typeof(ICreditLimitProvider);
            this.providers = creditLimitProviderType.Assembly.ExportedTypes
                .Where(x => creditLimitProviderType.IsAssignableFrom(x)
                            && !x.IsInterface
                            && !x.IsAbstract)
                .Select(x =>
                {
                    var parameterlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                    return parameterlessCtor is not null
                        ? Activator.CreateInstance(x)
                        : Activator.CreateInstance(x, userCreditService);
                })
                .Cast<ICreditLimitProvider>()
                .ToImmutableDictionary(k => k.NameRequirement, v => v);
        }

        public ICreditLimitProvider GetProviderByClientName(string clientName)
        {
            var provider = this.providers.GetValueOrDefault(clientName);
            return provider ?? this.providers[string.Empty];
        }
    }
}