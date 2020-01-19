using System.Threading.Tasks;

namespace model.choices.trash
{
    public class Leave : ITrashOption
    {
        bool ITrashOption.IsLegal(Game game) => true;

        async Task ITrashOption.Perform(Game game) => await Task.CompletedTask;

        string ITrashOption.Art => "Images/UI/thumb-up";
    }
}
