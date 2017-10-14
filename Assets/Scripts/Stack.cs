using UnityEngine;
using UnityEngine.EventSystems;

public class Stack : MonoBehaviour, IBeginDragHandler
{

    Deck deck;
    CardPrinter printer;

    private Card dragged;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        printer.PrintRunnerFacedown("Top of stack", this.transform);
    }

}
