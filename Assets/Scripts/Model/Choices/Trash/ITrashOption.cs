using System.Threading.Tasks;

namespace model.choices.trash
{
    public interface ITrashOption
    {
        bool IsLegal(Game game);
        Task Perform(Game game);
        string Art { get; }
    }
}