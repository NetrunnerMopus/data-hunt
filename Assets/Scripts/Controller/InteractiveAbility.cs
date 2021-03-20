using System.Threading.Tasks;
using model.play;

namespace controller
{
    public class InteractiveAbility : IInteractive
    {
        private Ability ability;
        public event Update Updated = delegate { };
        public DropZone Activation { get; }
        public bool Active { get; private set; }

        public InteractiveAbility(Ability ability, DropZone activation)
        {
            this.ability = ability;
            this.Activation = activation;
            this.Active = ability.Usable;
            ability.UsabilityChanged += UpdateUsability;
        }

        async Task IInteractive.Interact()
        {
            await ability.Trigger();
        }

        private void UpdateUsability(Ability ability, bool usable)
        {
            Active = usable;
            Updated();
        }
    }
}
