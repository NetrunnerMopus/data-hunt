using System.Collections.Generic;
using model.choices.steal;
using model.zones;

namespace model.cards.types
{
    public class Identity : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
        List<IInstallDestination> IType.FindInstallDestinations(Game game) => new List<IInstallDestination>();
        IStealOption IType.DefaultStealing(Card card) => new CannotSteal();
    }
}
