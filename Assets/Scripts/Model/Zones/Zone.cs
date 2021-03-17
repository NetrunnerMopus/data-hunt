using System;
using System.Collections.Generic;
using model.cards;

namespace model.zones
{
    public class Zone
    {
        public readonly string Name;
        public List<Card> Cards = new List<Card>();
        public int Count => Cards.Count;
        public event Action<Zone, Card> Added = delegate { };
        public event Action<Zone, Card> Removed = delegate { };
        public event Action<Zone> Changed = delegate { };

        public Zone(string name)
        {
            this.Name = name;
        }

        public void Add(Card card)
        {
            Cards.Add(card);
            Changed(this);
            Added(this, card);
        }

        public void Remove(Card card)
        {
            if (Cards.Contains(card))
            {
                Cards.Remove(card);
                Changed(this);
                Removed(this, card);
            }
            else
            {
                throw new System.Exception("Trying to remove a " + card.Name + " from " + Name + " but it's not in there");
            }
        }
    }
}
