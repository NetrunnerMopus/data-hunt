using UnityEngine;
using model;
using view;
using view.gui;

public class ActionCardConfig : MonoBehaviour
{
    public GameObject stack;
    public GameObject grip;
    public GameObject bank;
    public GameObject credits;

    public ActionCardView View()
    {
        var draw = new AbilityHighlight(stack.AddComponent<Highlight>());
        var credit = new AbilityHighlight(bank.AddComponent<Highlight>());
        return new ActionCardView(draw, credit);
    }
}
