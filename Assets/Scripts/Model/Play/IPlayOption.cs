using System.Threading.Tasks;

namespace model.play
{
    public interface IPlayOption
    {
        bool Legal { get; }
        Task Resolve();
    }
}
