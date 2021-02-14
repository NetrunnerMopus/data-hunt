using System.Collections.Generic;
using model.zones;
using model.choices.steal;

namespace model.cards
{
    public interface IType
    {
        bool Playable { get; }
        bool Installable { get; }
        bool Rezzable { get; }
        IList<IInstallDestination> FindInstallDestinations(Game game);
        IList<IStealOption> DefaultStealing(Card card, Game game);
    }
}
