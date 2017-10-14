using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Netrunner : MonoBehaviour
{

    public Canvas canvas;
    public EventSystem eventSystem;

    private int cards = 0;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            CreateCard();
        }

    }

    private void CreateCard()
    {
        cards++;
        GameObject gameObject = new GameObject("Card #" + cards);
        gameObject.transform.SetParent(canvas.transform);


        gameObject.layer = 5;

        Image image = gameObject.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Images/Cards/11024");
        image.preserveAspect = true;


        RectTransform rectangle = gameObject.GetComponent<RectTransform>();

        rectangle.anchorMin = new Vector2(0.2f, 0.4f);
        rectangle.anchorMax = new Vector2(0.2f, 0.4f);
        rectangle.anchoredPosition = new Vector2(cards * 90.0f, cards * 10.0f);
        rectangle.pivot = new Vector2(0.5f, 0.5f);
        rectangle.sizeDelta = new Vector2(300, 419);

        Card card = gameObject.AddComponent<Card>();
    }

}
