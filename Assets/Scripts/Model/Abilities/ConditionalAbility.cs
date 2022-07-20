using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.play;

namespace model.abilities {
    public class ConditionalAbility : IAbility {

        public bool Active { get; }
        public ISource Source { get; }

        private readonly TriggerCondition condition;
        private readonly IInstruction instruction;

        public ConditionalAbility(TriggerCondition condition, IInstruction instruction) {
            this.condition = condition;
            this.instruction = instruction;
        }

        internal List<Instance> InstantiatePerOccurrence() {
            if (Active) {
                return condition
                    .occurrences
                    .Select(it => new Instance(this))
                    .ToList();
            } else {
                throw new System.Exception("Tried to instantiate an inactive ability " + this);
            }
        }

        async public Task Resolve() {
            await instruction.Resolve();
        }

        public class Instance {
            public bool Pending { get; set; }
            public bool Imminent { get; set; }
            public bool Resolving { get; set; }
            private readonly ConditionalAbility conditionalAbility;

            public Instance(ConditionalAbility conditionalAbility) {
                this.conditionalAbility = conditionalAbility;
            }

            public async Task Trigger() {
                if (Pending) {
                    Pending = false;
                    Imminent = true;
                    // TODO prevent window I guess?
                    Resolving = true;
                    Imminent = false;
                    await conditionalAbility.Resolve();
                    Resolving = false;
                } else {
                    throw new System.Exception("Tried to trigger a non-pending instance " + this);
                }
            }
        }
    }

    public class TriggerCondition {
        internal IList<IOccurrence> occurrences = new List<IOccurrence>();

        public interface IOccurrence {

        }
    }

    public interface IInstruction {
        Task Resolve();
    }
}
