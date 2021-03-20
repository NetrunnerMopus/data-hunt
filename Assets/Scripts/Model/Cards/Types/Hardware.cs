using System.Collections.Generic;
using model.steal;
using model.zones;

namespace model.cards.types
{
    public class Hardware : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
        IList<IInstallDestination> IType.FindInstallDestinations() => new List<IInstallDestination>() { game.runner.zones.rig };
        IList<IStealOption> IType.DefaultStealing(Card card) => new List<IStealOption>();
        private Game game;
        public Hardware(Game game) => this.game = game;
    }
}
