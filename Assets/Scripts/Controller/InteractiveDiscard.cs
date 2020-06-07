using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using model.cards;
using model.zones.runner;

namespace controller
{
    public class InteractiveDiscard : IInteractive, IGripDiscardObserver
    {
        private Card card;
        private Grip grip;
        private Heap heap;
        private IList<Toggle> toggles = new List<Toggle>();

        public InteractiveDiscard(Card card, Game game)
        {
            this.card = card;
            this.grip = game.runner.zones.grip;
            this.heap = game.runner.zones.heap;
            this.grip.ObserveDiscarding(this);
        }

        void IInteractive.Observe(Toggle toggle)
        {
            toggles.Add(toggle);
        }

        async Task IInteractive.Interact()
        {
            grip.Discard(card, heap);
            await Task.CompletedTask;
        }

        void IInteractive.UnobserveAll()
        {
            grip.UnobserveDiscarding(this);
        }

        void IGripDiscardObserver.NotifyDiscarding(bool discarding)
        {
            foreach (var toggle in toggles)
            {
                toggle(discarding);
            }
        }
    }
}
