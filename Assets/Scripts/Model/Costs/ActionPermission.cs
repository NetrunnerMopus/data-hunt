using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class ActionPermission : ICost
    {
        private bool allowed = false;

        public bool Payable {get; private set;}

        public event Action<ICost, bool> ChangedPayability = delegate { };

        async Task ICost.Pay()
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
            ChangedPayability(this, allowed);
        }
    }
}
