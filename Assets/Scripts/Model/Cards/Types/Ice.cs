using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Ice : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => true;
        IEnumerable<IInstallDestination> IType.FindInstallDestinations(Game game) => game.corp.zones.ListIceInstalls();
    }
}
