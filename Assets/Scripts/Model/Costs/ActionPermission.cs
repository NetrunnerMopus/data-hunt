using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class ActionPermission : ICost
    {
        public bool Payable { get; private set; } = false;
        public event Action<ICost, bool> ChangedPayability = delegate { };

        async Task ICost.Pay()
        {
            if (!Payable)
            {
                throw new System.Exception("Tried to fire an action while it was forbidden");
            }
            Revoke();
            await Task.CompletedTask;
        }

        public void Grant()
        {
            Payable = true;
            ChangedPayability(this, Payable);
        }

        private void Revoke()
        {
            Payable = false;
            ChangedPayability(this, Payable);
        }
    }
}
