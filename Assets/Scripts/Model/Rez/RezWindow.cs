using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
using model.play;

namespace model.rez
{
    public class RezWindow
    {
        private RezWindowPermission permission = new RezWindowPermission();
        private TaskCompletionSource<bool> windowClosing;
        private bool used = false;
        private List<Ability> rezzes = new List<Ability>();

        internal RezWindow() { }

        public event AsyncAction<RezWindow, List<Ability>> Opened;
        public event Action<RezWindow> Closed = delegate { };

        public ICost Permission() => permission;

        async public Task<bool> Open()
        {
            windowClosing = new TaskCompletionSource<bool>();
            if (rezzes.Count == 0)
            {
                return false;
            }
            used = false;
            permission.Grant();
            await Opened?.Invoke(this, rezzes);
            var result = await windowClosing.Task;
            permission.Revoke();
            Closed(this);
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

        internal void Add(Ability rez)
        {
            rezzes.Add(rez);
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
}
