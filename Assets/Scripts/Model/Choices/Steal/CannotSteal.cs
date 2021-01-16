using System.Threading.Tasks;

namespace model.choices.steal
{
    public class CannotSteal : IStealOption
    {
        string IStealOption.Art => "Images/UI/thumb-up";

        bool IStealOption.IsLegal(Game game) => true;

        Task IStealOption.Perform(Game game) => Task.CompletedTask;
    }
}
