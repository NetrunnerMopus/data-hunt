using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.play;

namespace model.timing
{
    public abstract class PriorityWindow : ITimingStructure<PriorityWindow>
    {
        protected IList<CardAbility> abilities = new List<CardAbility>();
        private TaskCompletionSource<bool> pass;
        public event Action<PriorityWindow, CardAbility> Added = delegate { };
        public event Action<PriorityWindow, CardAbility> Removed = delegate { };
        public event AsyncAction<PriorityWindow> Opened;
        public event AsyncAction<PriorityWindow> Closed;
        public string Name { get; }

        public PriorityWindow(string name)
        {
            Name = name;
        }
        public abstract Task Open();

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
    }
}
