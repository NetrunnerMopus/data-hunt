using UnityEngine;
using UnityEngine.UI;

public class CardPrinter
{
    public GameObject PrintCorpFacedown(string name, Transform parent)
    {
        return Print(name, "Images/UI/corp-card-back", parent);
    }

    public GameObject PrintRunnerFacedown(string name, Transform parent)
    {
        return Print(name, "Images/UI/runner-card-back", parent);
    }

    public GameObject Print(string name, string asset, Transform parent)
    {
        var gameObject = new GameObject(name)
        {
            layer = 5
        };

        var image = gameObject.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>(asset);
        image.preserveAspect = true;
        var canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        gameObject.transform.SetParent(parent);
        return gameObject;
    }
}
