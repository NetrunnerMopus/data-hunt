using System.Threading.Tasks;
using model.player;

namespace model.timing {
    public abstract class ITurn : ITimingStructure {
        public abstract ClickPool Clicks { get; }
        public abstract Side Side { get; }
        public abstract IPilot Owner { get; }
        public event AsyncAction<ITurn> Began;

        async protected Task Begin() {
            await Began?.Invoke(this);
        }
    }
}
