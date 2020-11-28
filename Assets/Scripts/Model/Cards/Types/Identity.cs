using System.Collections.Generic;
using model.zones;

namespace model.cards.types
{
    public class Identity : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        IEnumerable<IInstallDestination> IType.FindInstallDestinations(Game game) => new List<IInstallDestination>();
    }
}
