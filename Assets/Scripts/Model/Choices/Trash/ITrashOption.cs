using System.Threading.Tasks;

namespace model.choices.trash
{
    public interface ITrashOption
    {
        bool IsLegal();
        /// <returns>true if card was trashed</returns>
        Task<bool> Perform();
        string Art { get; }
    }
}
