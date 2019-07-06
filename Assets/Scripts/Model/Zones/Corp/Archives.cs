using model.cards;
using model.player;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Archives : IServer
    {
        public Zone Zone { get; } = new Zone("Archives");
        public IceColumn Ice { get; } = new IceColumn();
        IEnumerable<Card> IServer.Access(int accessCount, IPilot pilot)
        {
            throw new System.NotImplementedException();
        }
    }
}
