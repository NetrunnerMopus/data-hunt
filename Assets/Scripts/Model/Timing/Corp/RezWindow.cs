using model.cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing.corp
{
    public class RezWindow
    {
        private RezWindowPermission permission = new RezWindowPermission();
        private TaskCompletionSource<bool> windowClosing;
        private bool used = false;
        private HashSet<IRezWindowObserver> windowObservers = new HashSet<IRezWindowObserver>();
        private List<Card> rezzables = new List<Card>();

        public ICost Permission() => permission;

        async public Task<bool> Open()
        {
            windowClosing = new TaskCompletionSource<bool>();
            if (rezzables.Count == 0)
            {
                return false;
            }
            used = false;
            permission.Grant();
            foreach (var observer in windowObservers)
            {
                observer.NotifyRezWindowOpened();
            }
            var result = await windowClosing.Task;
            permission.Revoke();
            foreach (var observer in windowObservers)
            {
                observer.NotifyRezWindowClosed();
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

        public void Add(Card rezzable)
        {
            rezzables.Add(rezzable);
        }

        public void Remove(Card rezzable)
        {
            rezzables.Remove(rezzable);
        }

        public void ObserveWindow(IRezWindowObserver observer)
        {
            windowObservers.Add(observer);
        }

        public void UnobserveWindow(IRezWindowObserver observer)
        {
            windowObservers.Remove(observer);
        }

        private class RezWindowPermission : ICost
        {
            private bool allowed = false;
            private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

            void ICost.Pay(Game game)
            {
                if (!allowed)
                {
                    throw new System.Exception("Tried to rez a card outside of a rez window");
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

    public interface IRezWindowObserver
    {
        void NotifyRezWindowOpened();
        void NotifyRezWindowClosed();
    }
}