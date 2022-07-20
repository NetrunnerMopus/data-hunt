using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards;
using model.choices;
using model.choices.trash;
using model.play;
using model.steal;
using model.timing;
using model.zones;

namespace model.player {
    public class NoPilot : IPilot {
        void IPilot.Play(Game game) { }

        IPlayOption IPilot.Choose(IList<IPlayOption> options) {
            throw new System.NotImplementedException();
        }

        Task IPilot.Receive(Priority priority) {
            throw new System.NotImplementedException();
        }

        Task<Ability> IPilot.TriggerFromSimultaneous(IEnumerable<Ability> abilities) {
            return Task.FromResult(abilities.First());
        }

        IDecision<string, Card> IPilot.ChooseACard() => new FailingChoice<string, Card>();
        IDecision<string, IInstallDestination> IPilot.ChooseAnInstallDestination() => new FailingChoice<string, IInstallDestination>();
        IDecision<Card, ITrashOption> IPilot.ChooseTrashing() => new FailingChoice<Card, ITrashOption>();
        IDecision<Card, IStealOption> IPilot.ChooseStealing() => new FailingChoice<Card, IStealOption>();
    }
}
