using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing
{
    public class PaidWindowPermission : ICost
    {
        private bool allowed = false;
        private bool paid = false;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        bool ICost.Payable(Game game) => allowed;
        
        async Task ICost.Pay(Game game)
        {
            if (!allowed)
            {
                throw new System.Exception("Tried to fire a paid ability while the window was closed");
            }
            paid = true;
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
            paid = false;
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

        public bool WasPaid() => paid;
    }
}