using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class ActionPermission : ICost
    {
        private bool allowed = false;
        public event Action<ICost, bool> PayabilityChanged = delegate { };

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
            PayabilityChanged(this, allowed);
        }
    }
}
