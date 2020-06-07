using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using model.play;

namespace controller
{
    public class InteractiveAbility : IInteractive, IUsabilityObserver
    {
        private Ability ability;
        private Game game;
        private IList<Toggle> toggles = new List<Toggle>();

        public InteractiveAbility(Ability ability, Game game)
        {
            this.ability = ability;
            this.game = game;
            ability.ObserveUsability(this, game);
        }

        void IInteractive.Observe(Toggle toggle)
        {
            toggles.Add(toggle);
        }

        async Task IInteractive.Interact()
        {
            await ability.Trigger(game);
        }

        void IInteractive.UnobserveAll()
        {
            ability.Unobserve(this);
        }

        void IUsabilityObserver.NotifyUsable(bool usable, Ability ability)
        {
            foreach (var toggle in toggles)
            {
                toggle(usable);
            }
        }
    }
}
