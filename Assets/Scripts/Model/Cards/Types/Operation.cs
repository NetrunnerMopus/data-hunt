using model.choices.steal;
using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Operation : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        IList<IInstallDestination> IType.FindInstallDestinations(Game game) => new List<IInstallDestination>();
        IList<IStealOption> IType.DefaultStealing(Card card, Game game) => new List<IStealOption>();
    }
}
