using System;
using model.cards;

namespace model.zones.runner
{
    public class Score
    {
        public readonly Zone zone = new Zone("Runner score");
        private int score = 0;
        public event Action StolenEnough = delegate { };

        public void Add(Card card, int agendaPoints)
        {
            card.FlipFaceUp(); // CR: 1.16.4
            card.MoveTo(zone);
            score += agendaPoints;
            if (score >= 7) // TODO Harmony MedTech
            {
                StolenEnough();
            }
        }
    }
}
