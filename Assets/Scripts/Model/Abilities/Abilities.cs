using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.timing;

namespace model.abilities {
    public class Abilities {
        private IList<ConditionalAbility> conditionals = new List<ConditionalAbility>();

        public void AddConditional(ConditionalAbility conditional) {
            conditionals.Add(conditional);
        }

        async public Task CheckReactions() {
            await CheckReactions(GatherPending());
        }

        private IList<ConditionalAbility.Instance> GatherPending() {
            return conditionals
                 .Where(it => it.Active)
                 .SelectMany(it => it.InstantiatePerOccurrence())
                 .ToList();
        }

        async private Task CheckReactions(IList<ConditionalAbility.Instance> pending) {
            if (pending.Count > 0) {
                var reactionWindow = new ReactionWindow(pending);
                await reactionWindow.Open();
                var newPending = GatherPending(); // CR: 9.6.4.a
                await CheckReactions(newPending);
            }
        }
    }
}
