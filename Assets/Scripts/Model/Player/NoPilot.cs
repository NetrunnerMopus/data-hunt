using model.cards;
using model.choices;
using model.choices.trash;
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

        IChoice<Card> IPilot.ChooseACard() => new FailingChoice<Card>();
        IChoice<IInstallDestination> IPilot.ChooseAnInstallDestination() => new FailingChoice<IInstallDestination>();
        IChoice<ITrashOption> IPilot.ChooseTrashing() => new FailingChoice<ITrashOption>();
    }
}