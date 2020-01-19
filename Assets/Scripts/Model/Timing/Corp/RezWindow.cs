using model.costs;
using model.play;
using model.play.corp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing.corp
{
    public class RezWindow
    {
        private RezWindowPermission permission = new RezWindowPermission();
        private TaskCompletionSource<bool> windowClosing;
        private bool used = false;
        private List<Rezzable> rezzables = new List<Rezzable>();
        private HashSet<IRezWindowObserver> windowObservers = new HashSet<IRezWindowObserver>();

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
                observer.NotifyRezWindowOpened(rezzables);
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

        public void Add(Rezzable rezzable)
        {
            rezzables.Add(rezzable);
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

            bool ICost.Payable(Game game) => allowed;

            async Task ICost.Pay(Game game)
            {
                if (!allowed)
                {
                    throw new System.Exception("Tried to rez a card outside of a rez window");
                }
                await Task.CompletedTask;
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
        void NotifyRezWindowOpened(List<Rezzable> rezzables);
        void NotifyRezWindowClosed();
    }
}