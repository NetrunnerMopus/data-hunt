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

        IChoice<string, Card> IPilot.ChooseACard() => new FailingChoice<string, Card>();
        IChoice<string, IInstallDestination> IPilot.ChooseAnInstallDestination() => new FailingChoice<string, IInstallDestination>();
        IChoice<Card, ITrashOption> IPilot.ChooseTrashing() => new FailingChoice<Card, ITrashOption>();
    }
}