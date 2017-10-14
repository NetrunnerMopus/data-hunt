using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private System.Random rng = new System.Random();

    private List<Card> cards;

    public Deck(List<Card> cards)
    {
        this.cards = cards;
    }

    public void Shuffle()
    {
        cards.Sort((card1, card2) => rng.Next().CompareTo(rng.Next()));
    }

    public Card Draw()
    {
        Card drawn = cards[0];
        cards.RemoveAt(0);
        return drawn;
    }

}
