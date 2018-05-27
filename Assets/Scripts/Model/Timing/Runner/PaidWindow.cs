using model.play;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing.runner
{
    public class PaidWindow
    {
        private TaskCompletionSource<bool> windowClosing;
        private bool used = false;
        private HashSet<IPaidWindowObserver> observers = new HashSet<IPaidWindowObserver>();
        private List<Ability> abilities = new List<Ability>();

        async internal Task<bool> Open()
        {
            windowClosing = new TaskCompletionSource<bool>();
            if (abilities.Count == 0)
            {
                return false;
            }
            used = false;
            foreach (var observer in observers)
            {
                observer.NotifyPaidWindowOpened();
            }
            await windowClosing.Task;
            foreach (var observer in observers)
            {
                observer.NotifyPaidWindowClosed();
            }
            return windowClosing.Task.Result;
        }

        internal void Use()
        {
            used = true;
        }

        internal void Pass()
        {
            windowClosing.SetResult(used);
        }

        internal void Add(Ability ability)
        {
            abilities.Add(ability);
        }

        internal void Remove(Ability ability)
        {
            abilities.Remove(ability);
        }

        internal void Observe(IPaidWindowObserver observer)
        {
            observers.Add(observer);
        }

        internal void Unobserve(IPaidWindowObserver observer)
        {
            observers.Remove(observer);
        }
    }

    internal interface IPaidWindowObserver
    {
        void NotifyPaidWindowOpened();
        void NotifyPaidWindowClosed();
    }
}