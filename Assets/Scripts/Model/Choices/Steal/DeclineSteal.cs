using System.Threading.Tasks;

namespace model.choices.steal
{
    public class DeclineSteal : IStealOption
    {
        string IStealOption.Art => "Images/UI/thumb-up";

        bool IStealOption.IsLegal(Game game) => true;

        Task<bool> IStealOption.Perform(Game game) => Task.FromResult(false);
    }
}
