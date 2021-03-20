using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace controller
{
    public class InteractiveAbility : IInteractive
    {
        private Ability ability;
        private IList<Update> updates = new List<Update>();
        public DropZone Activation { get; }
        public bool Active { get; private set; }

        public InteractiveAbility(Ability ability, DropZone activation)
        {
            this.ability = ability;
            this.Activation = activation;
            this.Active = ability.Usable;
            ability.UsabilityChanged += UpdateUsability;
        }

        void IInteractive.Observe(Update update)
        {
            updates.Add(update);
        }

        async Task IInteractive.Interact()
        {
            await ability.Trigger();
        }

        void IInteractive.UnobserveAll()
        {
            ability.UsabilityChanged -= UpdateUsability;
        }

        private void UpdateUsability(Ability ability, bool usable)
        {
            Active = usable;
            foreach (var update in updates)
            {
                update();
            }
        }
    }
}
