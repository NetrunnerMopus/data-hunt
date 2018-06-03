using model.cards;
using model.play;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing
{
    public class PaidWindow
    {
        private PaidWindowPermission permission = new PaidWindowPermission();
        private TaskCompletionSource<bool> windowClosing;
        private bool used = false;
        private HashSet<IPaidWindowObserver> windowObservers = new HashSet<IPaidWindowObserver>();
        private HashSet<IPaidAbilityObserver> abilityObservers = new HashSet<IPaidAbilityObserver>();
        private List<Ability> abilities = new List<Ability>();

        public ICost Permission() => permission;

        async public Task<bool> Open()
        {
            windowClosing = new TaskCompletionSource<bool>();
            if (abilities.Count == 0)
            {
                return false;
            }
            used = false;
            permission.Grant();
            foreach (var observer in windowObservers)
            {
                observer.NotifyPaidWindowOpened();
            }
            var result = await windowClosing.Task;
            permission.Revoke();
            foreach (var observer in windowObservers)
            {
                observer.NotifyPaidWindowClosed();
            }
            return result;
        }

        public void Use()
        {
            used = true;
        }

        public void Pass()
        {
            windowClosing.SetResult(used);
        }

        public void Add(Ability ability, Card source)
        {
            abilities.Add(ability);
            foreach (var observer in abilityObservers)
            {
                observer.NotifyPaidAbilityAvailable(ability, source);
            }
        }

        public void Remove(Ability ability)
        {
            abilities.Remove(ability);
        }

        public void ObserveWindow(IPaidWindowObserver observer)
        {
            windowObservers.Add(observer);
        }

        public void UnobserveWindow(IPaidWindowObserver observer)
        {
            windowObservers.Remove(observer);
        }

        public void ObserveAbility(IPaidAbilityObserver observer)
        {
            abilityObservers.Add(observer);
        }

        private class PaidWindowPermission : ICost
        {
            private bool allowed = false;
            private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

            void ICost.Pay(Game game)
            {
                if (!allowed)
                {
                    throw new System.Exception("Tried to fire a paid ability while the window was closed");
                }
            }

            void ICost.Observe(IPayabilityObserver observer, Game game)
            {
                observers.Add(observer);
                observer.NotifyPayable(allowed, this);
            }

            public void Grant()
            {
                allowed = true;
                Update();
            }

            public void Revoke()
            {
                allowed = false;
                Update();
            }

            private void Update()
            {
                foreach (var observer in observers)
                {
                    observer.NotifyPayable(allowed, this);
                }
            }
        }
    }

    public interface IPaidWindowObserver
    {
        void NotifyPaidWindowOpened();
        void NotifyPaidWindowClosed();
    }

    public interface IPaidAbilityObserver
    {
        void NotifyPaidAbilityAvailable(Ability ability, Card source);
    }
}