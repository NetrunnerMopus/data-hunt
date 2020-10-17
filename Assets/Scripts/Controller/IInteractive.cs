using System.Threading.Tasks;

namespace controller
{
    public interface IInteractive
    {
        DropZone Activation { get; }
        bool Active { get; }
        void Observe(Update update);
        Task Interact();
        void UnobserveAll();
    }

    public delegate void Update();
}
