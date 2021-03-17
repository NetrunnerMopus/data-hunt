using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model
{
    public interface IEffect
    {
        Task Resolve(Game game);
        bool Impactful { get; }
        event Action<IEffect, bool> ChangedImpact;
        IEnumerable<string> Graphics { get; }
    }
}
