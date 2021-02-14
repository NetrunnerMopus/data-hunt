using model.choices.steal;
using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Asset : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => true;
        IList<IInstallDestination> IType.FindInstallDestinations(Game game) => game.corp.zones.RemoteInstalls();
        IList<IStealOption> IType.DefaultStealing(Card card, Game game) => new List<IStealOption>();
    }
}
