using model.cards;
using System;
using System.Collections.Generic;

namespace model.zones.runner
{
    public class Rig : IInstallDestination
    {
        public readonly Zone zone = new Zone("Rig");

        void IInstallDestination.Host(Card card)
        {
            card.MoveTo(zone);
        }
    }
}