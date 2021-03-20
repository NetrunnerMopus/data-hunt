using System.Threading.Tasks;

namespace controller
{
    public interface IInteractive
    {
        DropZone Activation { get; }
        bool Active { get; }
        event Update Updated;
        Task Interact();
    }

    public delegate void Update();
}
