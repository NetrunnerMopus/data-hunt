using System.Threading.Tasks;

namespace model.steal
{
    public class DeclineSteal : IStealOption
    {
        string IStealOption.Art => "Images/UI/thumb-up";

        bool IStealOption.IsLegal() => true;

        Task<bool> IStealOption.Perform() => Task.FromResult(false);
    }
}
