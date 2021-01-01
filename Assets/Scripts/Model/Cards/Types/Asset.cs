using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Asset : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => true;
        List<IInstallDestination> IType.FindInstallDestinations(Game game) => game.corp.zones.RemoteInstalls();
        Stealable IType.Stealable => Stealable.CANNOT_STEAL;
    }
}