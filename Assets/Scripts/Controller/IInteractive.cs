using System.Threading.Tasks;

namespace controller
{
    public interface IInteractive
    {
        void Observe(Toggle toggle);
        Task Interact();
        void UnobserveAll();
    }

    public delegate void Toggle(bool active);
}
