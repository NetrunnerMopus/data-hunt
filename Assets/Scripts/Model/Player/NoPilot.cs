using model.cards;
using model.choices;
using model.choices.steal;
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

        IDecision<string, Card> IPilot.ChooseACard() => new FailingChoice<string, Card>();
        IDecision<string, IInstallDestination> IPilot.ChooseAnInstallDestination() => new FailingChoice<string, IInstallDestination>();
        IDecision<Card, ITrashOption> IPilot.ChooseTrashing() => new FailingChoice<Card, ITrashOption>();
        IDecision<Card, IStealOption> IPilot.ChooseStealing() => new FailingChoice<Card, IStealOption>();
    }
}
