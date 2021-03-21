using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing.corp
{
    public class RezWindow
    {
        private RezWindowPermission permission = new RezWindowPermission();
        private TaskCompletionSource<bool> windowClosing;
        private bool used = false;
        private List<Rezzable> rezzables = new List<Rezzable>();
        private IList<IRezWindowObserver> windowObservers = new List<IRezWindowObserver>();

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
            public bool Payable { get; private set; }
            public event Action<ICost, bool> ChangedPayability = delegate { };

            async Task ICost.Pay()
            {
                if (!Payable)
                {
                    throw new System.Exception("Tried to rez a card outside of a rez window");
                }
                await Task.CompletedTask;
            }

            public void Grant()
            {
                Payable = true;
                ChangedPayability(this, Payable);
            }

            public void Revoke()
            {
                Payable = false;
                ChangedPayability(this, Payable);
            }
        }
    }

    public interface IRezWindowObserver
    {
        void NotifyRezWindowOpened(List<Rezzable> rezzables);
        void NotifyRezWindowClosed();
    }
}
