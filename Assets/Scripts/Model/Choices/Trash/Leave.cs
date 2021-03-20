using System.Threading.Tasks;

namespace model.choices.trash
{
    public class Leave : ITrashOption
    {
        bool ITrashOption.IsLegal() => true;

        async Task<bool> ITrashOption.Perform() => await Task.FromResult(false);

        string ITrashOption.Art => "Images/UI/thumb-up";
    }
}
