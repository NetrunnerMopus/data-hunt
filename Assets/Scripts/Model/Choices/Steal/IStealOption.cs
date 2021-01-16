using System.Threading.Tasks;

namespace model.choices.steal
{
    public interface IStealOption
    {
        bool IsLegal(Game game);

        /// <returns>true if agenda was stolen</returns>
        Task<bool> Perform(Game game);
        string Art { get; }
    }

    public interface IStealModifier
    {
        IStealOption Modify(IStealOption option);
    }
}
