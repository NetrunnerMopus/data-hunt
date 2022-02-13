using System.Threading.Tasks;
using model.play;

namespace model.abilities {
    public interface IAbility {

        Task Resolve();
        ISource Source { get; }
        bool Active { get; }
    }
}
