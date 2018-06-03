using model.cards;
using model.play;
using model.timing;

namespace tests.observers
{
    class PaidAbilityObserver : IPaidAbilityObserver
    {
        public Ability NewestPaidAbility { get; private set; }

        void IPaidAbilityObserver.NotifyPaidAbilityAvailable(Ability ability, Card source)
        {
            NewestPaidAbility = ability;
        }
    }
}