using model.choices.steal;
using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Program : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
        IList<IInstallDestination> IType.FindInstallDestinations(Game game)
        {
            return new List<IInstallDestination>()
            {
                game.runner.zones.rig
            };
        }
        IList<IStealOption> IType.DefaultStealing(Card card, Game game) => new List<IStealOption>();
    }
}
