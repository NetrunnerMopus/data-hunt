using System.Threading.Tasks;

namespace model.timing
{
    public interface ITimingStructure<T> where T : ITimingStructure<T>
    {
        Task Open();
        event AsyncAction<T> Opened;
        event AsyncAction<T> Closed;
        string Name { get; }
    }
}
