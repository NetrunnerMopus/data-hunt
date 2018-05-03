using UnityEngine;
using controller;
using model.cards;
using model;

namespace view.log
{
    public class GripLog : IGripObserver
    {
        void IGripObserver.NotifyCardAdded(ICard card)
        {
            Debug.Log("Adding " + card + " to the grip");
        }
    }
}