using model.play;
using model.timing;

namespace tests.observers
{
    class PaidAbilityObserver
    {
        public CardAbility NewestPaidAbility { get; private set; }

        public PaidAbilityObserver(PaidWindow window)
        {
            window.Added += NotifyPaidAbilityAvailable;
        }

        void NotifyPaidAbilityAvailable(PaidWindow window, CardAbility ability)
        {
            NewestPaidAbility = ability;
        }
    }
}
