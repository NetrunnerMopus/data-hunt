using System.Threading.Tasks;

namespace model.choices.trash
{
    public interface ITrashOption
    {
        bool IsLegal(Game game);
        /// <returns>true if card was trashed</returns>
        Task<bool> Perform(Game game);
        string Art { get; }
    }
}
