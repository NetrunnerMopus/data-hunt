using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.cards;
using model.play;

namespace model.timing
{
    public class PaidWindow
    {
        private readonly string label;
        private PaidWindowPermission permission = new PaidWindowPermission();
        public event Action<PaidWindow> Opened = delegate { };
        public event Action<PaidWindow> Closed = delegate { };
        private HashSet<IPaidAbilityObserver> abilityObservers = new HashSet<IPaidAbilityObserver>();
        private List<Ability> abilities = new List<Ability>();
        private TaskCompletionSource<bool> pass;

        public PaidWindow(string label)
        {
            this.label = label;
        }

        public List<Ability> ListAbilities() => new List<Ability>(abilities);

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

        public void Add(Ability ability, Card source)
        {
            abilities.Add(ability);
            foreach (var observer in abilityObservers)
            {
                observer.NotifyPaidAbilityAvailable(ability, source);
            }
        }

        public void Remove(Ability ability)
        {
            abilities.Remove(ability);
        }

        public void ObserveAbility(IPaidAbilityObserver observer)
        {
            abilityObservers.Add(observer);
        }

        public override string ToString()
        {
            return "PaidWindow(label=" + label + ")";
        }
    }

    public interface IPaidAbilityObserver
    {
        void NotifyPaidAbilityAvailable(Ability ability, Card source);
    }
}
