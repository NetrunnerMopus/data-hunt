using UnityEngine;
using model.zones.corp;
using model.cards;

namespace view.gui
{
    public class ArchivesPile : MonoBehaviour, IArchivesObserver
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IArchivesObserver.NotifyCardAdded(ICard card)
        {
            var printer = GetComponent<CardPrinter>();
            if (card.Faceup)
            {
                printer.Print(card);
            }
            else
            {
                var facedown = printer.PrintCorpFacedown("Facedown in Archives");
                facedown.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
            }
        }
    }
}