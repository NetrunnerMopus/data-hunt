using model.choices.steal;
using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Hardware : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
        IStealOption IType.DefaultStealing(Card card) => new CannotSteal();
        List<IInstallDestination> IType.FindInstallDestinations(Game game)
        {
            return new List<IInstallDestination>()
            {
                game.runner.zones.rig
            };
        }
    }
}
