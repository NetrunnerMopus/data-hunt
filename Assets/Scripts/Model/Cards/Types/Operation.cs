using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Operation : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        List<IInstallDestination> IType.FindInstallDestinations(Game game) => new List<IInstallDestination>();
        Stealable IType.Stealable => Stealable.CANNOT_STEAL;
    }
}