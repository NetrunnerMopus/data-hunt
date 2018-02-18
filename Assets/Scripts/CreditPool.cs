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

    public bool CanPay(int cost)
    {
        return credits >= cost;
    }

    public void Pay(int cost)
    {
        if (CanPay(cost))
        {
            credits -= cost;
            UpdateText();
        }
        else
        {
            throw new System.Exception("Cannot pay " + cost + " credits while there's only " + credits + " in the pool");

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
