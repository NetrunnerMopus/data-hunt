using UnityEngine;
using model.cards;
using model.zones.runner;
using model.zones;

namespace view.log
{
    public class GripLog : IZoneAdditionObserver, IZoneRemovalObserver
    {
        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            Debug.Log("Adding " + card + " to the grip");
        }

        void IZoneRemovalObserver.NotifyCardRemoved(Card card)
        {
            Debug.Log("Removed " + card + " from the grip");
        }
    }
}