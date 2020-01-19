using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.costs
{
    public class ActionPermission : ICost
    {
        private bool allowed = false;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        bool ICost.Payable(Game game) => allowed;

        async Task ICost.Pay(Game game)
        {
            if (!allowed)
            {
                throw new System.Exception("Tried to fire an action while it was forbidden");
            }
            Revoke();
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

        private void Revoke()
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