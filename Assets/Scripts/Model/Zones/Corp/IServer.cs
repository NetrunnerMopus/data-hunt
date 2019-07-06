using System.Collections.Generic;
using model.cards;
using model.player;
using model.timing;

namespace model.zones.corp
{
    public interface IServer
    {
        Zone Zone { get; }
        IceColumn Ice { get; }
        IEnumerable<Card> Access(int accessCount, IPilot pilot);
    }
}
