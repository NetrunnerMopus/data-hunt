using System.Collections.Generic;
using model.player;
using model.timing;

namespace model.play
{
    public interface ISource
    {
        bool Active { get; }
        IList<ITimingStructure> Used { get; } // CR 9.1.6
        IPilot Controller { get; }
    }
}
