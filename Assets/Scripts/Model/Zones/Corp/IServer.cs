using System.Threading.Tasks;
using model.player;

namespace model.zones.corp
{
    public interface IServer
    {
        Zone Zone { get; }
        IceStack IceStack { get; }
        Task Access(int accessCount, IPilot pilot, Game game);
    }
}
