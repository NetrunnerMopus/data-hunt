using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using model.play;

namespace controller
{
    public class InteractiveAbility : IInteractive
    {
        private Ability ability;
        private Game game;
        private IList<Update> updates = new List<Update>();
        public DropZone Activation { get; }
        public bool Active { get; private set; }

        public InteractiveAbility(Ability ability, DropZone activation, Game game)
        {
            this.ability = ability;
            this.Activation = activation;
            this.Active = ability.Usable;
            this.game = game;
            ability.UsabilityChanged += UpdateUsability;
        }

        void IInteractive.Observe(Update update)
        {
            updates.Add(update);
        }

        async Task IInteractive.Interact()
        {
            await ability.Trigger(game);
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
