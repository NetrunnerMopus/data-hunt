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
        public DropZone Activation { get; }
        public bool Active { get; private set; }
        public event Update Updated = delegate { };

        public InteractiveDiscard(Card card, DropZone activation, Runner runner)
        {
            this.card = card;
            this.Activation = activation;
            this.Active = false;
            this.grip = runner.zones.grip;
            this.heap = runner.zones.heap;
            this.grip.ObserveDiscarding(this);
        }

        async Task IInteractive.Interact()
        {
            grip.Discard(card, heap);
            await Task.CompletedTask;
        }

        void IGripDiscardObserver.NotifyDiscarding(bool discarding)
        {
            Active = discarding;
            Updated();
        }
    }
}
