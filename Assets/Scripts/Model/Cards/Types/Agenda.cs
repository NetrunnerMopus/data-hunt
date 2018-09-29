using System.Collections.Generic;
using model.zones;

namespace model.cards.types
{
    public class Agenda : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
        List<IInstallDestination> IType.FindInstallDestinations(Game game) => game.corp.zones.RemoteInstalls();
    }
}