using System.Threading.Tasks;

namespace model.timing
{
    public interface ITurn
    {
        ClickPool Clicks { get; }
        Side Side { get; }
        Task Start();
    }
}
