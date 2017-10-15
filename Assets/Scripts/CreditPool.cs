using UnityEngine;
using UnityEngine.UI;

public class CreditPool
{
    private int credits = 0;
    private GameObject zone;

    public CreditPool(GameObject zone)
    {
        this.zone = zone;
    }

    public int MaxPayout()
    {
        return credits;
    }

    public void Pay(int cost)
    {
        if (cost > credits)
        {
            throw new System.Exception("");
        }
        else
        {
            credits -= cost;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        var text = zone.GetComponentInChildren<Text>();
        text.text = credits + " credits";
    }

    public void Gain(int income)
    {
        credits += income;
        UpdateText();
    }
}
