using System.Collections.Generic;
using model.stealing;
using model.zones;

namespace model.cards.types
{
    public class Event : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        IList<IInstallDestination> IType.FindInstallDestinations() => new List<IInstallDestination>();
        IList<IStealOption> IType.DefaultStealing(Card card) => new List<IStealOption>();
    }
}
