using System.Threading.Tasks;

namespace model.choices.steal
{
    public class CannotSteal : IStealOption
    {
        Task IStealOption.Perform(Game game) => Task.CompletedTask;
    }
}
