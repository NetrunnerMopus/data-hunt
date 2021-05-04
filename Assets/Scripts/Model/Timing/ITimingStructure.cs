using System.Threading.Tasks;

namespace model.timing
{
    public interface ITimingStructure<T> where T : ITimingStructure
    {
        event AsyncAction<T> Opened;
        event AsyncAction<T> Closed;
    }

    public interface ITimingStructure
    {
        Task Open();
        string Name { get; }
    }
}
