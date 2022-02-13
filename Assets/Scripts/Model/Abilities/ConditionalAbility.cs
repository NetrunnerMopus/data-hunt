using System.Collections.Generic;
using System.Linq;

namespace model.abilities {
    public class ConditionalAbility {

        public bool Active { get; private set; }
        private TriggerCondition condition;
        private IInstruction instruction;

        public ConditionalAbility(TriggerCondition condition, IInstruction instruction) {
            this.condition = condition;
            this.instruction = instruction;
        }

        internal List<Instance> InstantiatePerOccurrence() {
            return condition
                .occurrences
                .Select(it => it.Instantiate(instruction))
                .ToList();
        }

        public class Instance {
            public bool Pending { get; set; }
            public bool Imminent { get; set; }
            public bool Resolving { get; set; }
        }
    }

    public class TriggerCondition {
        internal IList<Occurrence> occurrences = new List<Occurrence>();
    }

    public interface IInstruction {
    }

    internal class Occurrence {
        object source;

        internal ConditionalAbility.Instance Instantiate(IInstruction instruction) {
            var instance = new ConditionalAbility.Instance();
            instance.Pending = true;
            instance.Imminent = false;
            instance.Resolving = false;
            return instance;
        }
    }
}
