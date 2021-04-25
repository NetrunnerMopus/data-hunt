using System;
using System.Threading.Tasks;
using model.play;

namespace model.timing
{
    public abstract class PriorityWindow : ITimingStructure<PriorityWindow>
    {
        public event AsyncAction<PriorityWindow> Opened;
        public event AsyncAction<PriorityWindow> Closed;
        public string Name { get; }

        public PriorityWindow(string name)
        {
            Name = name;
        }
        
        public abstract Task Open();
    }
}
