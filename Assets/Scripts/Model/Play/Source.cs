using model.player;

namespace model.play
{
    public interface ISource
    {
        bool Active { get; }
        bool Used { get; set; }
        IPilot Controller { get; }
    }
}
