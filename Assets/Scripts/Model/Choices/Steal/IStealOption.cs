using System.Threading.Tasks;

namespace model.choices.steal
{
    public interface IStealOption
    {
        bool IsLegal(Game game);
        Task Perform(Game game);
        string Art { get; }
    }
}
