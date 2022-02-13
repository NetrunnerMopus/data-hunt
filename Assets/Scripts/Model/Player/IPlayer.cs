using model.cards.text;
using model.timing;

namespace model.player
{
    public interface IPlayer {
        ITurn turn { get; }
    }
}
