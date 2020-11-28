using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Event : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        IEnumerable<IInstallDestination> IType.FindInstallDestinations(Game game) => new List<IInstallDestination>();
    }
}
