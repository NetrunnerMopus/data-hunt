using System.Threading.Tasks;

namespace model.timing {
    public abstract class ITimingStructure {
        event AsyncAction<ITimingStructure> Initiated;
        event AsyncAction<ITimingStructure> Completed;
        string Name { get; }

        async Task Initiate() {
            await Initiated?.Invoke(this);
            await Proceed();
            await Completed?.Invoke(this);
        }

        protected abstract Task Proceed();
    }
}
