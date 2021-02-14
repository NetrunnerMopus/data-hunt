using System.Threading.Tasks;

namespace model.choices.trash
{
    public class Leave : ITrashOption
    {
        bool ITrashOption.IsLegal(Game game) => true;

        async Task<bool> ITrashOption.Perform(Game game) => await Task.FromResult(false);

        string ITrashOption.Art => "Images/UI/thumb-up";
    }
}
