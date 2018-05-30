using model.cards;
using model.play;
using model.timing.runner;

namespace tests.observers
{
    class PaidAbilityObserver : IPaidAbilityObserver
    {
        public Ability NewestPaidAbility { get; private set; }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, ICard source)
        {
            NewestPaidAbility = ability;
        }
    }
}