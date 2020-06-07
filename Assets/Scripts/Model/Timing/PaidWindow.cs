using model.cards;
using model.play;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.timing
{
    public class PaidWindow
    {
        private readonly string label;
        private PaidWindowPermission permission = new PaidWindowPermission();
        private HashSet<IPaidWindowObserver> windowObservers = new HashSet<IPaidWindowObserver>();
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
            foreach (var observer in windowObservers)
            {
                observer.NotifyPaidWindowOpened(this);
            }
            await pass.Task;
            foreach (var observer in windowObservers)
            {
                observer.NotifyPaidWindowClosed(this);
            }
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

        public void ObserveWindow(IPaidWindowObserver observer)
        {
            windowObservers.Add(observer);
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

    public interface IPaidWindowObserver
    {
        void NotifyPaidWindowOpened(PaidWindow window);
        void NotifyPaidWindowClosed(PaidWindow window);
    }

    public interface IPaidAbilityObserver
    {
        void NotifyPaidAbilityAvailable(Ability ability, Card source);
    }
}
