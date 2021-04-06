using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing
{
    public class PaidWindow : ITimingStructure<PaidWindow>
    {
        private readonly string label;
        private PaidWindowPermission permission = new PaidWindowPermission();
        public event Action<PaidWindow> Opened = delegate { };
        public event Action<PaidWindow> Closed = delegate { };
        public event Action<PaidWindow, CardAbility> Added = delegate { };
        public event Action<PaidWindow, CardAbility> Removed = delegate { };
        private List<CardAbility> abilities = new List<CardAbility>();
        private TaskCompletionSource<bool> pass;

        public PaidWindow(string label)
        {
            this.label = label;
        }

        public List<CardAbility> ListAbilities() => new List<CardAbility>(abilities);

        public ICost Permission() => permission;

        async public Task<bool> AwaitPass()
        {
            pass = new TaskCompletionSource<bool>();
            permission.Grant();
            Opened(this);
            await pass.Task;
            Closed(this);
            permission.Revoke();
            return !permission.WasPaid();
        }

        public void Pass()
        {
            pass.SetResult(true);
        }

        public void Add(CardAbility ability)
        {
            abilities.Add(ability);
            Added(this, ability);
        }

        public void Remove(CardAbility ability)
        {
            abilities.Remove(ability);
            Removed(this, ability);
        }

        public override string ToString()
        {
            return "PaidWindow(label=" + label + ")";
        }
    }
}
