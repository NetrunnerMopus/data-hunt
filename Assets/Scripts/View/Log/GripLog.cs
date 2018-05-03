using UnityEngine;
using controller;
using model.cards;
using model;

namespace view.log
{
    public class GripLog : IGripAdditionObserver, IGripRemovalObserver
    {
        void IGripAdditionObserver.NotifyCardAdded(ICard card)
        {
            Debug.Log("Adding " + card + " to the grip");
        }

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            Debug.Log("Removed " + card + " from the grip");
        }
    }
}