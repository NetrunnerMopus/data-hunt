using System.Threading.Tasks;

namespace model.choices.steal
{
    public interface IStealOption
    {
        Task Perform(Game game);
    }
}
