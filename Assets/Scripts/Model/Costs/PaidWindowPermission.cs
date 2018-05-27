using System.Collections.Generic;

namespace model.costs
{
    public class PaidWindowPermission : ICost
    {
        private bool allowed = false;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        void ICost.Pay(Game game)
        {
            if (allowed)
            {
                Revoke();
            }
            else
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
            allowed = true;
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