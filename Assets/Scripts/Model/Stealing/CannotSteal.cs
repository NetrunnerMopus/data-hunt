using System.Threading.Tasks;

namespace model.stealing
{
    public class CannotSteal : IStealOption
    {
        string IStealOption.Art => "Images/UI/thumb-up";

        bool IStealOption.IsLegal() => true;

        Task<bool> IStealOption.Perform() => Task.FromResult(false);
    }
}
