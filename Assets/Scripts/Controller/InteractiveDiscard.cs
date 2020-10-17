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
        private IList<Update> updates = new List<Update>();
        public DropZone Activation { get; }
        public bool Active { get; private set; }

        public InteractiveDiscard(Card card, DropZone activation, Game game)
        {
            this.card = card;
            this.Activation = activation;
            this.Active = false;
            this.grip = game.runner.zones.grip;
            this.heap = game.runner.zones.heap;
            this.grip.ObserveDiscarding(this);
        }

        void IInteractive.Observe(Update toggle)
        {
            updates.Add(toggle);
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
            Active = discarding;
            foreach (var update in updates)
            {
                update();
            }
        }
    }
}
