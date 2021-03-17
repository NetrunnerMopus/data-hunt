using System.Collections.Generic;
using model.stealing;
using model.zones;

namespace model.cards
{
    public interface IType
    {
        bool Playable { get; }
        bool Installable { get; }
        bool Rezzable { get; }
        IList<IInstallDestination> FindInstallDestinations();
        IList<IStealOption> DefaultStealing(Card card);
    }
}
