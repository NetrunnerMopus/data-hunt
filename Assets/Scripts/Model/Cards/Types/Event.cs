using model.choices.steal;
using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Event : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        List<IInstallDestination> IType.FindInstallDestinations(Game game) => new List<IInstallDestination>();
        IStealOption IType.DefaultStealing(Card card) => new CannotSteal();
    }
}
