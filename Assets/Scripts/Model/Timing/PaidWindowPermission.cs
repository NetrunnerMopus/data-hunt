using System;
using System.Threading.Tasks;

namespace model.timing
{
    public class PaidWindowPermission : ICost
    {
        public bool Payable { get; private set; } = false;
        public event Action<ICost, bool> ChangedPayability;
        private bool paid = false;
        async Task ICost.Pay()
        {
            if (!Payable)
            {
                throw new System.Exception("Tried to fire a paid ability while the window was closed");
            }
            paid = true;
            await Task.CompletedTask;
        }

        public void Grant()
        {
            Payable = true;
            paid = false;
            Update();
        }

        public void Revoke()
        {
            Payable = false;
            Update();
        }

        private void Update()
        {
            ChangedPayability?.Invoke(this, Payable);
        }

        public bool WasPaid() => paid;
    }
}
