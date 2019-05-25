using model.cards;
using System.Collections.Generic;

namespace model.zones.corp
{
    public class Archives : IServer
    {
        public Zone Zone { get; } = new Zone("Archives");
        public IceColumn Ice { get; } = new IceColumn();
    }
}
