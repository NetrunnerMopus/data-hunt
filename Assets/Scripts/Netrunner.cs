using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Netrunner : MonoBehaviour
{

    public Canvas canvas;
    public Grip grip;
    public EventSystem eventSystem;
    private Deck deck = new Decks().DemoRunner();

    private int cards = 0;

    void Start()
    {
        deck.Shuffle();
        for (int i = 0; i < 5; i++)
        {
            var card = deck.Draw();
            AddToGrip(card);
        }
    }

    private void AddToGrip(Card card)
    {
        cards++;
        GameObject gameObject = new GameObject(card.name)
        {
            layer = 5
        };

        Image image = gameObject.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Images/Cards/" + card.id);
        image.preserveAspect = true;


        RectTransform rectangle = gameObject.GetComponent<RectTransform>();

        //     rectangle.anchorMin = new Vector2(0.2f, 0.4f);
        //     rectangle.anchorMax = new Vector2(0.2f, 0.4f);
        //    rectangle.anchoredPosition = new Vector2(cards * 90.0f, cards * 10.0f);
        //  rectangle.pivot = new Vector2(0.5f, 0.5f);
        //    rectangle.sizeDelta = new Vector2(300, 419);

        LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
        //   layoutElement.flexibleHeight = 100.0f;
        //   layoutElement.flexibleWidth = 100.0f;

        gameObject.transform.SetParent(grip.gameObject.transform);

        FaceupCard faceupCard = gameObject.AddComponent<FaceupCard>();
        CanvasGroup canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        // layoutElement.fl
        // layoutElement.
    }

}
