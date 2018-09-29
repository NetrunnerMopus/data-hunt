using model.cards;
using model.zones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.player
{
    public class NoPilot : IPilot
    {
        void IPilot.Play(Game game) { }

        Task<IEffect> IPilot.TriggerFromSimultaneous(List<IEffect> effects)
        {
            return Task.FromResult(effects.First());
        }

        IChoice<Card> IPilot.ChooseACard()
        {
            throw new System.NotImplementedException();
        }

        IChoice<IInstallDestination> IPilot.ChooseAnInstallDestination()
        {
            throw new System.NotImplementedException();
        }
    }
}