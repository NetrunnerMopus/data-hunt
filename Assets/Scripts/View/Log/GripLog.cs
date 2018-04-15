using UnityEngine;
using controller;
using model.cards;

namespace view.log
{
    public class GripLog : IGripView
    {
        void IGripView.Add(ICard card)
        {
            Debug.Log("Adding " + card + " to the grip");
        }
    }
}