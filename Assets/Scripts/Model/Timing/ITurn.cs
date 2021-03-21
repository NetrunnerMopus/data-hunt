using System.Threading.Tasks;

namespace model.timing
{
    public interface ITurn
    {
        ClickPool Clicks { get; }
        Side Side { get; }
        Task Start();

        /// <summary>
        /// Tells observers the turn has just started.
        /// It's different from "when begins" triggers, because it's not for card effects. It's for tracking the earliest moment when the turn is considered active.
        /// For example for tracking/resetting counters per turn.
        /// </summary>
        event AsyncAction<ITurn> Started; 

        /// <summary>
        /// Registers a game effect to be fired at the beginning of the turn, e.g. PAD Campaign.
        /// </summary>
        void WhenBegins(IEffect effect);
    }
}
