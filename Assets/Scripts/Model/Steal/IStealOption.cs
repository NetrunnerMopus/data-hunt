using System.Threading.Tasks;

namespace model.steal
{
    public interface IStealOption
    {
        bool IsLegal();

        /// <returns>true if agenda was stolen</returns>
        Task<bool> Perform();
        string Art { get; }
    }

    public interface IStealModifier
    {
        IStealOption Modify(IStealOption option);
    }
}
