using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Hardware : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;

        IEnumerable<IInstallDestination> IType.FindInstallDestinations(Game game)
        {
            return new List<IInstallDestination>()
            {
                game.runner.zones.rig
            };
        }
    }
}
