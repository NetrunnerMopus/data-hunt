using System.Collections.Generic;
using model.zones;

namespace model.cards
{
    public interface IType
    {
        bool Playable { get; }
        bool Installable { get; }
        bool Rezzable { get; }
        IEnumerable<IInstallDestination> FindInstallDestinations(Game game);
    }
}
